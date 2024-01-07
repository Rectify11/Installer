System.Gadget.settingsUI = "settings.html";
System.Gadget.onSettingsClosed = AE;
var AR = new ActiveXObject("WScript.Shell");
var AS = "8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c";
var AT = "a1841308-3541-4fab-bc81-f71556f20b4a";
var AU = "381b4222-f694-41f0-9685-ff5bb260df2e";
var AV = "";
var AW = 0;
var AX = false;
var AY;
var AZ = "orb";
var Aa = false;
function AA() {
    AD();
    AC();
    setInterval("AC()", 1000);
}
function AB() {
    AG();
}
function AC() {
    try {
        AB();
    } catch (err) { }
}
function AD() {
    try {
        var Ab = System.Gadget.Settings.readString("back");
        if (Ab != "") {
            AZ = Ab;
        } if (System.Gadget.Settings.readString("windowsLevel") != "") Aa = System.Gadget.Settings.read("windowsLevel");
        At.src = "images/backgrounds/" + AZ + "_back.png";
        At_.src = "images/backgrounds/" + AZ + "_back_.png";
    } catch (err) { }
}
function AE() {
    AD();
}
function AF() {
    System.Shell.execute("powercfg.cpl");
}
function AG() {
    var Ac = System.Machine.PowerStatus.isPowerLineConnected;
    var Ad = System.Machine.PowerStatus.isBatteryCharging;
    var Ae = System.Machine.PowerStatus.batteryPercentRemaining;
    var Af = System.Machine.PowerStatus.batteryCapacityTotal;
    var Ag = AH();
    if (Ag != AV) {
        BA.src = "images/profiles/highperf_d.png";
        BB.src = "images/profiles/balanced_d.png";
        BC.src = "images/profiles/pwrsav_d.png";
        if (Ag == AS) BA.src = "images/profiles/highperf.png";
        if (Ag == AU) BB.src = "images/profiles/balanced.png";
        if (Ag == AT) BC.src = "images/profiles/pwrsav.png";
    }
    AV = Ag;
    if (Ae && Ae != 255) {
        var Ah = AQ(Ae, Ac, Ag);
        if (Ac) {
            Au.innerText = "Plugged in";
            Az.style.visibility = "visible";
            if (!AX) Ax.src = "images/status/bat16net.png";
            if (Ad) {
            Aw.innerText = "Charging...";
                if (!AX) AM();
            } else if (Ae != 100) {
            Aw.innerText = "Not Charging!";
                if (AX) AN();
            } else {
            Aw.innerText = "Fully Charged!";
                if (AX) AN();
            }
        } else {
            Au.innerText = "Unplugged";
            if (AX) AN();
            Az.style.visibility = "hidden";
            if (Ae >= 95) Ax.src = "images/status/bat16full.png";
            else if (Ae >= 75) Ax.src = "images/status/bat16high.png";
            else if (Ae >= 50) Ax.src = "images/status/bat16med.png";
            else if (Ae >= 25) Ax.src = "images/status/bat16low.png";
            if (Ah == "yellow") {
                Ax.src = "images/status/bat16warn.png";
                Au.innerText = "Warning!";
            } else if (Ah == "red") {
                Ax.src = "images/status/bat16warn.png";
                Au.innerText = "Critical!";
            } if (Af < 0) Aw.innerText = "Calculating...";
            else {
                Aw.innerText = "~ " + parseInt(Af / 3600) + ":";
                if ((Af = parseInt((Af % 3600) / 60)) < 10) {
                    Aw.innerText += "0";
                }
                Aw.innerText += Af + "h remaining";
            }
        } if (Aa) {
            if (Ae < 19) Ay.src = "images/battery/bat1" + Ah + ".png";
            else if (Ae < 30) Ay.src = "images/battery/bat2" + Ah + ".png";
            else if (Ae < 41) Ay.src = "images/battery/bat3" + Ah + ".png";
            else if (Ae < 52) Ay.src = "images/battery/bat4" + Ah + ".png";
            else if (Ae < 63) Ay.src = "images/battery/bat5" + Ah + ".png";
            else if (Ae < 74) Ay.src = "images/battery/bat6" + Ah + ".png";
            else if (Ae < 85) Ay.src = "images/battery/bat7" + Ah + ".png";
            else if (Ae < 96) Ay.src = "images/battery/bat8" + Ah + ".png";
            else if (Ae < 100) Ay.src = "images/battery/bat9" + Ah + ".png";
            else if (Ae == 100) Ay.src = "images/battery/bat10" + Ah + ".png";
        } else {
            if (Ae >= 95) Ay.src = "images/battery/bat10" + Ah + ".png";
            else if (Ae >= 85) Ay.src = "images/battery/bat9" + Ah + ".png";
            else if (Ae >= 75) Ay.src = "images/battery/bat8" + Ah + ".png";
            else if (Ae >= 65) Ay.src = "images/battery/bat7" + Ah + ".png";
            else if (Ae >= 55) Ay.src = "images/battery/bat6" + Ah + ".png";
            else if (Ae >= 45) Ay.src = "images/battery/bat5" + Ah + ".png";
            else if (Ae >= 35) Ay.src = "images/battery/bat4" + Ah + ".png";
            else if (Ae >= 25) Ay.src = "images/battery/bat3" + Ah + ".png";
            else if (Ae >= 15) Ay.src = "images/battery/bat2" + Ah + ".png";
            else Ay.src = "images/battery/bat1" + Ah + ".png";
        } Av.innerText = Ae + "%";
    } else {
        Au.innerText = "Power Supply";
        Av.innerText = "0%";
        Aw.innerText = "No Battery found!";
        Az.style.visibility = "visible";
        Ay.src = "images/battery/bat0.png";
        Ax.src = "images/status/bat16net.png";
        if (AX) AN();
    }
}
function AH() {
    regkey = "HKLM\\SYSTEM\\CurrentControlSet\\Control\\Power\\User\\PowerSchemes\\ActivePowerScheme";
    try {
        return AR.RegRead(regkey);
    } catch (err) {
        return "";
    }
}
function AI(Ao) {
    try {
        System.Shell.execute("powercfg.exe", "/setactive " + Ao, "", "open");
    } catch (err) {
        txtRemaining.innerText = err.message;
    }
}
function AJ() {
    AI(AS);
}
function AK() {
    AI(AU);
}
function AL() {
    AI(AT);
}
function AM() {
    AY = setInterval("AO()", 500);
    AX = true;
}
function AN() {
    clearInterval(AY);
    AX = false;
}
function AO() {
    if (AW == 3) Ax.src = "images/status/bat16full.png";
    else if (AW == 2) Ax.src = "images/status/bat16high.png";
    else if (AW == 1) Ax.src = "images/status/bat16med.png";
    else if (AW == 0) Ax.src = "images/status/bat16low.png";
    AW++;
    if (AW >= 4) AW = 0;
}
function AP(Ap) {
    try {
        return parseInt(AR.RegRead(Ap));
    } catch (err) {
        return -1;
    }
}
function AQ(Aq, Ar, As) {
    var Ai = "HKLM\\SYSTEM\\CurrentControlSet\\Control\\Power\\User\\PowerSchemes\\";
    var Aj = "\\e73a048d-bf27-4f12-9731-8b2076e8891f\\8183ba9a-e910-48da-8769-14ae6dc1170a\\";
    var Ak = "\\e73a048d-bf27-4f12-9731-8b2076e8891f\\9a66d8d7-4ff7-4ef9-b5a2-5a326ca2a469\\";
    var Al = "ACSettingIndex";
    if (!Ar) Al = "DCSettingIndex";
    var Am = AP(Ai + As + Aj + Al);
    if (Am == -1) Am = 10;
    var An = AP(Ai + As + Ak + Al);
    if (An == -1) An = 5;
    if (Aq <= An) return "red";
    else if (Aq <= Am) return "yellow";
    else return "green";
}