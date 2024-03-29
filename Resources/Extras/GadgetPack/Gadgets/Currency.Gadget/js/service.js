
function CurrencyService()
{
	var me = this;
	
    var request = null;
	
	this.hsCurrencies;
	this.OnDataReady = null;
	this.IsAvailable = false;
	this.DateString = "";
	
	function FireOnDataReady() 
	{
		me.OnDataReady();
	}

    this.Convert = function(fFromAmount, sFromSymbol, sToSymbol)
    {
		if (fFromAmount == '')
			fFromAmount = '0';
		var decimals = 0;
		if (System.Gadget.docked)
		{
			decimals = 3;
		}
		else
		{
			decimals = 5;
        }

        fFromAmount = fFromAmount.replace(',', '.');
        if (fFromAmount.charAt(fFromAmount.length-1) == '.')
            fFromAmount = fFromAmount.substring(0, fFromAmount.length - 1);

        fFromAmount = fFromAmount.replace('$', '');
        fFromAmount = fFromAmount.replace('€', '');

        if (!/^(\-|\+)?([0-9]+(\.[0-9]+)?|Infinity)$/.test(fFromAmount))
            return 'NaN';

        value = parseFloat(fFromAmount);
        if (!isFinite(value))
            return 'NaN';

        value = value / me.hsCurrencies[sFromSymbol].PerEuro * me.hsCurrencies[sToSymbol].PerEuro;
        return '' + value.toFixed(decimals)
	}
	
    this.GetCurrencies = function ()
    {
        if (request != null)
            return;

        request = new XMLHttpRequest();
        request.open("GET", "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");
        request.setRequestHeader("If-Modified-Since", "Sat, 1 Jan 2000 00:00:00 GMT");
        request.onreadystatechange = OnRequestDone;
        request.send(null);
    }

    function OnRequestDone()
    {
        if (request.readyState != 4)
        {
            return;
        }

		newCurrencies = new Array();

        if (request.status < 200 || request.status >= 300)
        {
            // no internet connection
            request.abort();
            request = null;
            FireOnDataReady();
            return;
        }

        div = window.document.createElement('div');
        div.innerHTML = request.responseText;

        var cubes = div.getElementsByTagName("Cube");
        var time = cubes[1].getAttribute("time");
        for (var i = 2; i < cubes.length; i++)
        {
            var currency = new Object();
		    currency.Symbol = cubes[i].getAttribute("currency");
		    currency.Name = getLocalizedString(currency.Symbol);
		    currency.PerEuro = cubes[i].getAttribute("rate");
            currency.NameForSorting = currency.Name;
            newCurrencies[currency.Symbol] = currency;
        }

        {
            // add Euro
            var currency = new Object();
		    currency.Symbol = "EUR";
		    currency.Name = getLocalizedString(currency.Symbol);
		    currency.PerEuro = 1;
		    currency.NameForSorting = currency.Name;
            newCurrencies[currency.Symbol] = currency;
        }

        /*var euroPerDollar = 1 / newCurrencies["USD"].PerEuro;
        for (i in newCurrencies)
	    {
		    var currency = newCurrencies[i];
		    currency.PerDollar = currency.PerEuro*euroPerDollar;
        }*/
		
		
        me.hsCurrencies = newCurrencies;
        me.DateString = time;
        me.IsAvailable = true;

        request.abort();
        request = null;

    	//setTimeout('g_oService.OnDataReady()', 1000);
        FireOnDataReady();


        /*function parseIsoDatetime(dtstr)
        {
            var dt = dtstr.split(/[: T-]/);
            //return new Date         (dt[0], dt[1] - 1, dt[2], dt[3] || 0, dt[4] || 0, dt[5] || 0, 0);
            return new Date(Date.UTC(dt[0], dt[1] - 1, dt[2], dt[3] || 0, dt[4] || 0, dt[5] || 0, 0));
        }

        var today = parseIsoDatetime("2025-05-01");
        var t = today.toLocaleDateString("en-US");

        var options = { year: 'numeric', month: 'numeric', day: 'numeric' };

        var sd = today.toUTCString();
        var df = 5;*/
    }
}

