function AA() {
    var Ab = System.Gadget.Settings.readString("back");
    if (Ab != "") BG.value = Ab;
    if (System.Gadget.Settings.readString("windowsLevel") != "")
        BH.checked = System.Gadget.Settings.read("windowsLevel");
}

System.Gadget.onSettingsClosing = BD;

function BD(BE) {
    if (BE.closeAction == BE.Action.commit) {
        System.Gadget.Settings.writeString("back", BG.value);
        System.Gadget.Settings.write("windowsLevel", BH.checked);
    }
    BE.cancel = false;
}
