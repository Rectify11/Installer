using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Vanara.Interop.DesktopWindowManager
{
	[ProvideProperty("GlassEnabled", typeof(Form))]
	[ToolboxItem(true)]
	[ProvideProperty("GlassMargins", typeof(Form))]
	[Description("Extender for a Form that adds Aero glass properties.")]
	public class GlassExtenderProvider : Component, IExtenderProvider
	{
		private readonly Dictionary<Control, GlassExtenderProvider.GlassFormProperties> formProps = new();

		[Category("Behavior")]
		[Description("Indicates whether extending glass into the client area is enabled.")]
		[DefaultValue(true)]
		[DisplayName("GlassEnabled")]
		public bool GetGlassEnabled(Form form)
		{
			GlassExtenderProvider.GlassFormProperties glassFormProperties;
			return !this.formProps.TryGetValue((Control)form, out glassFormProperties) || glassFormProperties.GlassEnabled;
		}

		[Description("Specifies if clicking and dragging within the margin will move the form. ")]
		[DefaultValue(true)]
		[DisplayName("GlassMarginMovesForm")]
		[Category("Behavior")]
		public bool GetGlassMarginMovesForm(Form form)
		{
			GlassExtenderProvider.GlassFormProperties glassFormProperties;
			return !this.formProps.TryGetValue((Control)form, out glassFormProperties) || glassFormProperties.GlassMarginMovesForm;
		}

		[Category("Layout")]
		[DisplayName("GlassMargins")]
		[Description("Specifies the interior glass margin of the form. Set to -1 for full window glass.")]
		[DefaultValue(typeof(Padding), "0")]
		public Padding GetGlassMargins(Form form)
		{
			GlassExtenderProvider.GlassFormProperties glassFormProperties;
			return this.formProps.TryGetValue((Control)form, out glassFormProperties) ? glassFormProperties.GlassMargins : Padding.Empty;
		}

		public void SetGlassEnabled(Form form, bool value)
		{
			this.GetFormProperties(form).GlassEnabled = value;
			this.GlassifyForm(form);
		}

		public void SetGlassMarginMovesForm(Form form, bool value) => this.GetFormProperties(form).GlassMarginMovesForm = value;

		public void SetGlassMargins(Form form, Padding value)
		{
			var glassFormProperties = form != null ? this.GetFormProperties(form) : throw new ArgumentNullException(nameof(form));
			if (value == Padding.Empty)
			{
				glassFormProperties.GlassMargins = Padding.Empty;
				this.UnhookForm(form);
			}
			else
			{
				glassFormProperties.GlassMargins = value;
				form.Paint += new PaintEventHandler(this.form_Paint);
				form.MouseDown += new MouseEventHandler(this.form_MouseDown);
				form.MouseMove += new MouseEventHandler(this.form_MouseMove);
				form.MouseUp += new MouseEventHandler(this.form_MouseUp);
				form.Resize += new EventHandler(this.form_Resize);
				form.Shown += new EventHandler(this.form_Shown);
			}
			form.Invalidate();
		}
		bool IExtenderProvider.CanExtend(object form) => form != this && form is Form;

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				foreach (var key in this.formProps.Keys)
				{
					if (key is Form form && !form.IsDisposed)
						this.UnhookForm(form);
				}
			}
			base.Dispose(disposing);
		}

		private static Rectangle GetNonGlassArea(
			Form form,
			GlassExtenderProvider.GlassFormProperties prop)
		{
			return prop == null ? form.ClientRectangle : new Rectangle(form.ClientRectangle.Left + prop.GlassMargins.Left, form.ClientRectangle.Top + prop.GlassMargins.Top, form.ClientRectangle.Width - prop.GlassMargins.Horizontal, form.ClientRectangle.Height - prop.GlassMargins.Vertical);
		}

		private void form_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left)
				return;
			var formProperties = this.GetFormProperties(sender as Form);
			if (!formProperties.GlassMarginMovesForm)
				return;
			formProperties.FormMoveTracking = true;
			formProperties.FormMoveLastMousePos = ((Control)sender).PointToScreen(e.Location);
		}

		private void form_MouseMove(object sender, MouseEventArgs e)
		{
			if (!(sender is Form form))
				return;
			var formProperties = this.GetFormProperties(form);
			if (!formProperties.FormMoveTracking || GlassExtenderProvider.GetNonGlassArea(form, formProperties).Contains(e.Location))
				return;
			var screen = form.PointToScreen(e.Location);
			var p = new Point(screen.X - formProperties.FormMoveLastMousePos.X, screen.Y - formProperties.FormMoveLastMousePos.Y);
			var location = form.Location;
			location.Offset(p);
			form.Location = location;
			formProperties.FormMoveLastMousePos = screen;
		}

		private void form_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left)
				return;
			this.GetFormProperties(sender as Form).FormMoveTracking = false;
		}

		private void form_Paint(object sender, PaintEventArgs e) => this.GlassifyForm(sender as Form, e.Graphics);

		private void form_Resize(object sender, EventArgs e)
		{
			if ((!(sender is Form form) || !Vanara.Interop.DesktopWindowManager.DesktopWindowManager.IsCompositionEnabled() || !this.GetGlassEnabled(form)))
				return;
			this.InvalidateNonGlassClientArea(form);
		}

		private void form_Shown(object sender, EventArgs e) => this.GlassifyForm(sender as Form);

		private GlassExtenderProvider.GlassFormProperties GetFormProperties(
			Form form)
		{
			GlassExtenderProvider.GlassFormProperties formProperties;
			if (!this.formProps.TryGetValue((Control)form, out formProperties))
				this.formProps.Add((Control)form, formProperties = new GlassExtenderProvider.GlassFormProperties());
			return formProperties;
		}

		private void GlassifyForm(Form form, Graphics g = null)
		{
			if ((!Vanara.Interop.DesktopWindowManager.DesktopWindowManager.IsCompositionEnabled() || !this.GetGlassEnabled(form)))
				return;
			if (g == null)
				g = form.CreateGraphics();
			GlassExtenderProvider.GlassFormProperties prop;
			if (!this.formProps.TryGetValue((Control)form, out prop))
				return;
			if (prop.GlassMargins == new Padding(-1))
			{
				g.FillRectangle(Brushes.Black, form.ClientRectangle);
			}
			else
			{
				using (var region = new Region(form.ClientRectangle))
				{
					region.Exclude(GlassExtenderProvider.GetNonGlassArea(form, prop));
					g.FillRegion(Brushes.Black, region);
				}
			}
			form.ExtendFrameIntoClientArea(prop.GlassMargins);
		}

		private void InvalidateNonGlassClientArea(Form form)
		{
			var glassMargins = this.GetGlassMargins(form);
			if (glassMargins == Padding.Empty)
				return;
			var rc = Rectangle.Empty;
			ref var local = ref rc;
			var left = glassMargins.Left;
			var top = glassMargins.Top;
			var clientRectangle = form.ClientRectangle;
			var width = clientRectangle.Width - glassMargins.Right;
			clientRectangle = form.ClientRectangle;
			var height = clientRectangle.Height - glassMargins.Bottom;
			local = new Rectangle(left, top, width, height);
			form.Invalidate(rc, false);
		}

		private void UnhookForm(Form form)
		{
			form.MouseDown -= new MouseEventHandler(this.form_MouseDown);
			form.MouseMove -= new MouseEventHandler(this.form_MouseMove);
			form.MouseUp -= new MouseEventHandler(this.form_MouseUp);
			form.Shown -= new EventHandler(this.form_Shown);
			form.Resize -= new EventHandler(this.form_Resize);
			form.Paint -= new PaintEventHandler(this.form_Paint);
		}

		private class GlassFormProperties
		{
			public Point FormMoveLastMousePos = Point.Empty;
			public bool FormMoveTracking;
			public bool GlassEnabled = true;
			public bool GlassMarginMovesForm = true;
			public Padding GlassMargins = Padding.Empty;
		}
	}
}
