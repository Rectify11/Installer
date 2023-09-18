using System.Drawing;
using static Vanara.Interop.NativeMethods;

namespace System.Windows.Forms.VisualStyles
{
	internal static partial class VisualStyleRendererExtension
	{
		internal delegate void DrawWrapperMethod(SafeDCHandle hdc);

		internal static void DrawWrapper(IDeviceContext dc, Rectangle bounds, DrawWrapperMethod func)
		{
			using var sdc = new SafeDCHandle(dc);
			// Create a memory DC so we can work off screen
			using var memoryHdc = sdc.GetCompatibleDCHandle();
			// Create a device-independent bitmap and select it into our DC
			var info = new BITMAPINFO(bounds.Width, -bounds.Height);
			using (new SafeDCObjectHandle(memoryHdc, CreateDIBSection(sdc, info, 0, out var pBits)))
			{
				// Call method
				func(memoryHdc);

				// Copy to foreground
				BitBlt(sdc, bounds.Left, bounds.Top, bounds.Width, bounds.Height, memoryHdc, 0, 0, RasterOperationMode.SRCCOPY);
			}
		}
	}
}