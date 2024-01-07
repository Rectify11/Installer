nsp = 'Old browser!';
dl = document.layers;
oe = window.opera ? 1 : 0;
da = document.all &&! oe;
ge = document.getElementById;
ws = window.sidebar ? true : false;
tN = navigator.userAgent.toLowerCase();
izN = tN.indexOf('netscape') >= 0 ? true : false;
zis = tN.indexOf('msie 7') >= 0 ? true : false;
zis8 = tN.indexOf('msie 8') >= 0 ? true : false;
zis |= zis8;
if (ws &&! izN){
  quogl = 'iuy'
}
;
var msg = '';
function nem(){
  return true
}
;
window.onerror = nem;
zOF = window.location.protocol.indexOf("file") !=- 1 ? true : false;
i7f = zis &&! zOF ? true : false;
System.Gadget.settingsUI = "settings.html";
try {
  var CIMV2Service = GetObject("winmgmts:\\\\.\\root\\CIMV2");
}
catch (err){
}
var size = 1;
function onLoad(){
  size = System.Gadget.Settings.read("size");
  if (size != "");
  else size = "1";
  if (size <= "4");
  else size = "4";
  FlyoutBac = System.Gadget.Settings.read("FlyoutBac");
  if (FlyoutBac != "");
  else FlyoutBac = "#080808";
  fixFlyoutBac = System.Gadget.Settings.read("fixFlyoutBac");
  if (fixFlyoutBac != "")sFlyoutBac = fixFlyoutBac;
  else sFlyoutBac = FlyoutBac;
  FlyoutTit = System.Gadget.Settings.read("FlyoutTit");
  if (FlyoutTit != "");
  else FlyoutTit = "#87cefa";
  fixFlyoutTit = System.Gadget.Settings.read("fixFlyoutTit");
  if (fixFlyoutTit != "")sFlyoutTit = fixFlyoutTit;
  else sFlyoutTit = FlyoutTit;
  FlyoutDet = System.Gadget.Settings.read("FlyoutDet");
  if (FlyoutDet != "");
  else FlyoutDet = "#ffcc00";
  fixFlyoutDet = System.Gadget.Settings.read("fixFlyoutDet");
  if (fixFlyoutDet != "")sFlyoutDet = fixFlyoutDet;
  else sFlyoutDet = FlyoutDet;
  try {
    var readCaption = CIMV2Service.ExecQuery("SELECT Caption FROM Win32_Processor");
    getCaption = (new Enumerator(readCaption)).item().Caption;
    Caption.innerHTML = 
    "Caption: <a href='javascript:cgetCaption()' style='text-decoration:none'><font color="
     + sFlyoutDet + ">" + getCaption + "</a>";
  }
  catch (err){
    Caption.innerHTML = "Caption:";
  }
  try {
    var readName = CIMV2Service.ExecQuery("SELECT Name FROM Win32_Processor");
    getName = (new Enumerator(readName)).item().Name;
    Name.innerHTML = 
    "Name: <a href='javascript:cgetName()' style='text-decoration:none'><font color=" + 
    sFlyoutDet + ">" + getName + "</a>";
  }
  catch (err){
    Name.innerHTML = "Name:";
  }
  try {
    var readNumberOfCores = CIMV2Service.ExecQuery(
    "SELECT NumberOfCores FROM Win32_Processor");
    getNumberOfCores = (new Enumerator(readNumberOfCores)).item().NumberOfCores;
    NumberOfCores.innerHTML = "Number Of Cores: <a href='javascript:cgetNumberOfCores()' style='text-decoration:none'><font color=" + sFlyoutDet + ">" + getNumberOfCores + "</a>";
  }
  catch (err){
    NumberOfCores.innerHTML = "Number Of Cores:";
  }
  try {
    var readNumberOfLogicalProcessors = CIMV2Service.ExecQuery(
    "SELECT NumberOfLogicalProcessors FROM Win32_Processor");
    getNumberOfLogicalProcessors = (new Enumerator(readNumberOfLogicalProcessors)).item().
    NumberOfLogicalProcessors;
    NumberOfLogicalProcessors.innerHTML = "Number Of Logical Processors: <a href='javascript:cgetNumberOfLogicalProcessors()' style='text-decoration:none'><font color=" + sFlyoutDet + ">" + getNumberOfLogicalProcessors + 
    "</a>";
  }
  catch (err){
    NumberOfLogicalProcessors.innerHTML = "Number Of Logical Processors:";
  }
  try {
    var readProcessorId = CIMV2Service.ExecQuery("SELECT ProcessorId FROM Win32_Processor"
    );
    getProcessorId = (new Enumerator(readProcessorId)).item().ProcessorId;
    ProcessorId.innerHTML = "Processor Id: <a href='javascript:cProcessorId()' style='text-decoration:none'><font color=" + sFlyoutDet + ">" + getProcessorId + "</a>";
  }
  catch (err){
    ProcessorId.innerHTML = "Processor Id:";
  }
  try {
    var readManufacturer = CIMV2Service.ExecQuery(
    "SELECT Manufacturer FROM Win32_Processor");
    getManufacturer = (new Enumerator(readManufacturer)).item().Manufacturer;
    Manufacturer.innerHTML = "Manufacturer: <a href='javascript:cgetManufacturer()' style='text-decoration:none'><font color=" + sFlyoutDet + ">" + getManufacturer + "</a>";
  }
  catch (err){
    Manufacturer.innerHTML = "Manufacturer:";
  }
  try {
    var readMaxClockSpeed = CIMV2Service.ExecQuery(
    "SELECT MaxClockSpeed FROM Win32_Processor");
    getMaxClockSpeed = (new Enumerator(readMaxClockSpeed)).item().MaxClockSpeed;
    MaxClockSpeed.innerHTML = "Max Clock Speed: <a href='javascript:cgetMaxClockSpeed()' style='text-decoration:none'><font color=" + sFlyoutDet + ">" + getMaxClockSpeed + "MHz" + "</a>";
  }
  catch (err){
    MaxClockSpeed.innerHTML = "Max Clock Speed:";
  }
  try {
    var readExtClock = CIMV2Service.ExecQuery("SELECT ExtClock FROM Win32_Processor");
    getExtClock = (new Enumerator(readExtClock)).item().ExtClock;
    ExtClock.innerHTML = "Front-Side Bus: <a href='javascript:cgetExtClock()' style='text-decoration:none'><font color=" + sFlyoutDet + ">" + getExtClock + "MHz" + "</a>";
  }
  catch (err){
    ExtClock.innerHTML = "Front-Side Bus:";
  }
  try {
    var readL2CacheSize = CIMV2Service.ExecQuery("SELECT L2CacheSize FROM Win32_Processor"
    );
    getL2CacheSize = (new Enumerator(readL2CacheSize)).item().L2CacheSize;
    L2CacheSize.innerHTML = "L2 Cache Size: <a href='javascript:cgetL2CacheSize()' style='text-decoration:none'><font color=" + sFlyoutDet + ">" + getL2CacheSize + "kB" + "</a>";
  }
  catch (err){
    L2CacheSize.innerHTML = "L2 Cache Size:";
  }
  try {
    var readL3CacheSize = CIMV2Service.ExecQuery("SELECT L3CacheSize FROM Win32_Processor"
    );
    getL3CacheSize = (new Enumerator(readL3CacheSize)).item().L3CacheSize;
    L3CacheSize.innerHTML = "L3 Cache Size: <a href='javascript:cgetL3CacheSize()' style='text-decoration:none'><font color=" + sFlyoutDet + ">" + getL3CacheSize + "kB" + "</a>";
    }
  catch (err){
    L3CacheSize.innerHTML = "L3 Cache Size:";
  }
  try {
    var readCurrentVoltage = CIMV2Service.ExecQuery(
    "SELECT CurrentVoltage FROM Win32_Processor");
    getCurrentVoltage = ((new Enumerator(readCurrentVoltage)).item().CurrentVoltage / 10);
    CurrentVoltage.innerHTML = "Current Voltage: <a href='javascript:cgetCurrentVoltage()' style='text-decoration:none'><font color=" + sFlyoutDet + ">" + getCurrentVoltage + "V" + "</a>";
  }
  catch (err){
    CurrentVoltage.innerHTML = "Current Voltage:";
  }
  try {
    var readOSCaption = CIMV2Service.ExecQuery("SELECT Caption FROM Win32_OperatingSystem"
    );
    getOSCaption = (new Enumerator(readOSCaption)).item().Caption;
    OSCaption.innerHTML = 
    "Name: <a href='javascript:cgetOSCaption()' style='text-decoration:none'><font color="
     + sFlyoutDet + ">" + getOSCaption + "</a>";
  }
  catch (err){
    OSCaption.innerHTML = "Name:";
  }
  try {
    var readCSDVersion = CIMV2Service.ExecQuery(
    "SELECT CSDVersion FROM Win32_OperatingSystem");
    getCSDVersion = (new Enumerator(readCSDVersion)).item().CSDVersion;
    CSDVersion.innerHTML = "CSD Version: <a href='javascript:cgetCSDVersion()' style='text-decoration:none'><font color=" + sFlyoutDet + ">" + getCSDVersion + "</a>";
  }
  catch (err){
    CSDVersion.innerHTML = "CSD Version:";
  }
  try {
    var readOSVersion = CIMV2Service.ExecQuery("SELECT Version FROM Win32_OperatingSystem"
    );
    getOSVersion = (new Enumerator(readOSVersion)).item().Version;
    OSVersion.innerHTML = 
    "Version: <a href='javascript:cgetOSVersion()' style='text-decoration:none'><font color="
     + sFlyoutDet + ">" + getOSVersion + "</a>";
  }
  catch (err){
    OSVersion.innerHTML = "Version:";
  }
  try {
    var readOSSerialNumber = CIMV2Service.ExecQuery(
    "SELECT SerialNumber FROM Win32_OperatingSystem");
    getOSSerialNumber = (new Enumerator(readOSSerialNumber)).item().SerialNumber;
    OSSerialNumber.innerHTML = "Serial Number: <a href='javascript:cgetOSSerialNumber()' style='text-decoration:none'><font color=" + sFlyoutDet + ">" + getOSSerialNumber + "</a>";
  }
  catch (err){
    OSSerialNumber.innerHTML = "Serial Number:";
  }
  try {
    var readInstallDate = CIMV2Service.ExecQuery(
    "SELECT InstallDate FROM Win32_OperatingSystem");
    getInstallDate = (new Enumerator(readInstallDate)).item().InstallDate;
    InstallDate.innerHTML = "Install Date: <a href='javascript:cgetInstallDate()' style='text-decoration:none'><font color=" + sFlyoutDet + ">" + getInstallDate.replace(
    /^(\d{4})(\d{2})(\d{2})(\d{2})(\d{2})(\d{2})(.*)$/, "$1-$2-$3 $4:$5:$6") + "</a>";
  }
  catch (err){
    InstallDate.innerHTML = "Install Date:";
  }
  try {
    var readLastBootUpTime = CIMV2Service.ExecQuery(
    "SELECT LastBootUpTime FROM Win32_OperatingSystem");
    getLastBootUpTime = (new Enumerator(readLastBootUpTime)).item().LastBootUpTime;
    LastBootUpTime.innerHTML = "Last Boot Up Time: <a href='javascript:cgetLastBootUpTime()' style='text-decoration:none'><font color=" + sFlyoutDet + ">" + getLastBootUpTime.replace(
    /^(\d{4})(\d{2})(\d{2})(\d{2})(\d{2})(\d{2})(.*)$/, "$1-$2-$3 $4:$5:$6") + "</a>";
  }
  catch (err){
    LastBootUpTime.innerHTML = "Last Boot Up Time:";
  }
  try {
    var readNumberOfProcesses = CIMV2Service.ExecQuery(
    "SELECT NumberOfProcesses FROM Win32_OperatingSystem");
    getNumberOfProcesses = (new Enumerator(readNumberOfProcesses)).item().
    NumberOfProcesses;
    NumberOfProcesses.innerHTML = "Number Of Processes: <a href='javascript:cgetNumberOfProcesses()' style='text-decoration:none'><font color=" + sFlyoutDet + ">" + getNumberOfProcesses + "</a>";
  }
  catch (err){
    NumberOfProcesses.innerHTML = "Number Of Processes:";
  }
  try {
    var readNumberOfUsers = CIMV2Service.ExecQuery(
    "SELECT NumberOfUsers FROM Win32_OperatingSystem");
    getNumberOfUsers = (new Enumerator(readNumberOfUsers)).item().NumberOfUsers;
    NumberOfUsers.innerHTML = "Number Of Users: <a href='javascript:cgetNumberOfUsers()' style='text-decoration:none'><font color=" + sFlyoutDet + ">" + getNumberOfUsers + "</a>";
  }
  catch (err){
    NumberOfUsers.innerHTML = "Number Of Users:";
  }
  try {
    var readOSArchitecture = CIMV2Service.ExecQuery(
    "SELECT OSArchitecture FROM Win32_OperatingSystem");
    getOSArchitecture = (new Enumerator(readOSArchitecture)).item().OSArchitecture;
    OSArchitecture.innerHTML = "OS Architecture: <a href='javascript:cgetOSArchitecture()' style='text-decoration:none'><font color=" + sFlyoutDet + ">" + getOSArchitecture + "</a>";
  }
  catch (err){
    OSArchitecture.innerHTML = "OS Architecture:";
  }
  try {
    var readBBManufacturer = CIMV2Service.ExecQuery(
    "SELECT Manufacturer FROM Win32_BaseBoard");
    getBBManufacturer = (new Enumerator(readBBManufacturer)).item().Manufacturer;
    BBManufacturer.innerHTML = "Manufacturer: <a href='javascript:cgetBBManufacturer()' style='text-decoration:none'><font color=" + sFlyoutDet + ">" + getBBManufacturer + "</a>";
  }
  catch (err){
    BBManufacturer.innerHTML = "Manufacturer:";
  }
  try {
    var readProduct = CIMV2Service.ExecQuery("SELECT Product FROM Win32_BaseBoard");
    getProduct = (new Enumerator(readProduct)).item().Product;
    Product.innerHTML = 
    "Product: <a href='javascript:cgetProduct()' style='text-decoration:none'><font color="
     + sFlyoutDet + ">" + getProduct + "</a>";
  }
  catch (err){
    Product.innerHTML = "Product:";
  }
  try {
    var readBBSerialNumber = CIMV2Service.ExecQuery(
    "SELECT SerialNumber FROM Win32_BaseBoard");
    getBBSerialNumber = (new Enumerator(readBBSerialNumber)).item().SerialNumber;
    BBSerialNumber.innerHTML = "Serial Number: <a href='javascript:cgetBBSerialNumber()' style='text-decoration:none'><font color=" + sFlyoutDet + ">" + getBBSerialNumber + "</a>";
  }
  catch (err){
    BBSerialNumber.innerHTML = "Serial Number:";
  }
  try {
    var readBIOSName = CIMV2Service.ExecQuery("SELECT Name FROM Win32_BIOS");
    getBIOSName = (new Enumerator(readBIOSName)).item().Name;
    BIOSName.innerHTML = 
    "Name: <a href='javascript:cgetBIOSName()' style='text-decoration:none'><font color=" + 
    sFlyoutDet + ">" + getBIOSName + "</a>";
  }
  catch (err){
    BIOSName.innerHTML = "Name:";
  }
  try {
    var readBIOSManufacturer = CIMV2Service.ExecQuery(
    "SELECT Manufacturer FROM Win32_BIOS");
    getBIOSManufacturer = (new Enumerator(readBIOSManufacturer)).item().Manufacturer;
    BIOSManufacturer.innerHTML = "Manufacturer: <a href='javascript:cgetBIOSManufacturer()' style='text-decoration:none'><font color=" + sFlyoutDet + ">" + getBIOSManufacturer + "</a>";
  }
  catch (err){
    BIOSManufacturer.innerHTML = "Manufacturer:";
  }
  try {
    var readBIOSVersion = CIMV2Service.ExecQuery("SELECT Version FROM Win32_BIOS");
    getBIOSVersion = (new Enumerator(readBIOSVersion)).item().Version;
    BIOSVersion.innerHTML = "Version: <a href='javascript:cgetBIOSVersion()' style='text-decoration:none'><font color="     + sFlyoutDet + ">" + getBIOSVersion + "</a>";
  }
  catch (err){
    BIOSVersion.innerHTML = "Version:";
  }
  try {
    var readBIOSReleaseDate = CIMV2Service.ExecQuery("SELECT ReleaseDate FROM Win32_BIOS"
    );
    getBIOSReleaseDate = (new Enumerator(readBIOSReleaseDate)).item().ReleaseDate;
    BIOSReleaseDate.innerHTML = "Release Date: <a href='javascript:cgetBIOSReleaseDate()' style='text-decoration:none'><font color=" + sFlyoutDet + ">" + getBIOSReleaseDate.replace(
    /^(\d{4})(\d{2})(\d{2})(\d{2})(\d{2})(\d{2})(.*)$/, "$1-$2-$3 $4:$5:$6") + "</a>";
  }
  catch (err){
    BIOSReleaseDate.innerHTML = "Release Date:";
  }
  try {
    var readBIOSSerialNumber = CIMV2Service.ExecQuery(
    "SELECT SerialNumber FROM Win32_BIOS");
    getBIOSSerialNumber = (new Enumerator(readBIOSSerialNumber)).item().SerialNumber;
    BIOSSerialNumber.innerHTML = "Serial Number: <a href='javascript:cgetBIOSSerialNumber()' style='text-decoration:none'><font color=" + sFlyoutDet + ">" + getBIOSSerialNumber + "</a>";
  }
  catch (err){
    BIOSSerialNumber.innerHTML = "Serial Number:";
  }
  try {
    var readCSManufacturer = CIMV2Service.ExecQuery(
    "SELECT Manufacturer FROM Win32_ComputerSystem");
    getCSManufacturer = (new Enumerator(readCSManufacturer)).item().Manufacturer;
    CSManufacturer.innerHTML = "Manufacturer: <a href='javascript:cgetCSManufacturer()' style='text-decoration:none'><font color=" + sFlyoutDet + ">" + getCSManufacturer + "</a>";
  }
  catch (err){
    CSManufacturer.innerHTML = "Manufacturer:";
  }
  try {
    var readModel = CIMV2Service.ExecQuery("SELECT Model FROM Win32_ComputerSystem");
    getModel = (new Enumerator(readModel)).item().Model;
    Model.innerHTML = 
    "Model: <a href='javascript:cgetModel()' style='text-decoration:none'><font color=" + 
    sFlyoutDet + ">" + getModel + "</a>";
  }
  catch (err){
    Model.innerHTML = "Model:";
  }
  try {
    var readSystemType = CIMV2Service.ExecQuery(
    "SELECT SystemType FROM Win32_ComputerSystem");
    getSystemType = (new Enumerator(readSystemType)).item().SystemType;
    SystemType.innerHTML = "System Type: <a href='javascript:cgetSystemType()' style='text-decoration:none'><font color=" + sFlyoutDet + ">" + getSystemType + "</a>";
  }
  catch (err){
    SystemType.innerHTML = "System Type:";
  }
  sizeMode();
}
function cgetCaption(){
  window.clipboardData.setData('Text', getCaption);
}
function cgetName(){
  window.clipboardData.setData('Text', getName);
}
function cgetNumberOfCores(){
  window.clipboardData.setData('Text', getNumberOfCores + " Cores");
}
function cgetNumberOfLogicalProcessors(){
  window.clipboardData.setData('Text', getNumberOfLogicalProcessors + 
  " Logical Processors");
}
function cgetProcessorId(){
  window.clipboardData.setData('Text', getProcessorId);
}
function cgetManufacturer(){
  window.clipboardData.setData('Text', getManufacturer);
}
function cgetMaxClockSpeed(){
  window.clipboardData.setData('Text', getMaxClockSpeed + "MHz");
}
function cgetExtClock(){
  window.clipboardData.setData('Text', getExtClock + "MHz");
}
function cgetL2CacheSize(){
  window.clipboardData.setData('Text', getL2CacheSize + "kB");
}
function cgetL3CacheSize(){
  window.clipboardData.setData('Text', getL3CacheSize + "kB");
}
function cgetCurrentVoltage(){
  window.clipboardData.setData('Text', getCurrentVoltage / 10 + "V");
}
function cgetOSCaption(){
  window.clipboardData.setData('Text', getOSCaption);
}
function cgetCSDVersion(){
  window.clipboardData.setData('Text', getCSDVersion);
}
function cgetOSVersion(){
  window.clipboardData.setData('Text', getOSVersion);
}
function cgetOSSerialNumber(){
  window.clipboardData.setData('Text', getOSSerialNumber);
}
function cgetInstallDate(){
  window.clipboardData.setData('Text', getInstallDate.replace(
  /^(\d{4})(\d{2})(\d{2})(\d{2})(\d{2})(\d{2})(.*)$/, "$1-$2-$3 $4:$5:$6"));
}
function cgetLastBootUpTime(){
  window.clipboardData.setData('Text', getLastBootUpTime.replace(
  /^(\d{4})(\d{2})(\d{2})(\d{2})(\d{2})(\d{2})(.*)$/, "$1-$2-$3 $4:$5:$6"));
}
function cgetNumberOfProcesses(){
  window.clipboardData.setData('Text', getNumberOfProcesses + " Processess");
}
function cgetNumberOfUsers(){
  window.clipboardData.setData('Text', getNumberOfUsers + " Users");
}
function cgetOSArchitecture(){
  window.clipboardData.setData('Text', getOSArchitecture);
}
function cgetBBManufacturer(){
  window.clipboardData.setData('Text', getBBManufacturer);
}
function cgetProduct(){
  window.clipboardData.setData('Text', getProduct);
}
function cgetBBSerialNumber(){
  window.clipboardData.setData('Text', getBBSerialNumber);
}
function cgetBIOSName(){
  window.clipboardData.setData('Text', getBIOSName);
}
function cgetBIOSManufacturer(){
  window.clipboardData.setData('Text', getBIOSManufacturer);
}
function cgetBIOSVersion(){
  window.clipboardData.setData('Text', getBIOSVersion);
}
function cgetBIOSReleaseDate(){
  window.clipboardData.setData('Text', getBIOSReleaseDate.replace(
  /^(\d{4})(\d{2})(\d{2})(\d{2})(\d{2})(\d{2})(.*)$/, "$1-$2-$3 $4:$5:$6"));
}
function cgetBIOSSerialNumber(){
  window.clipboardData.setData('Text', getBIOSSerialNumber);
}
function cgetCSManufacturer(){
  window.clipboardData.setData('Text', getCSManufacturer);
}
function cgetModel(){
  window.clipboardData.setData('Text', getModel);
}
function cgetSystemType(){
  window.clipboardData.setData('Text', getSystemType);
}
function sizeMode(){
  document.body.style.width = parseInt(310 * size);
  document.body.style.height = parseInt(178 * size);
  document.body.style.backgroundColor = FlyoutBac;
  document.body.style.fontFamily = "Segoe UI";
  document.body.style.fontSize = parseInt(11 * size);
  document.body.style.color = sFlyoutTit;
  document.body.alinkColor = sFlyoutDet;
  document.vlinkColor = sFlyoutDet;
  document.getElementById('processor').style.fontSize = parseInt(14 * size);
  document.getElementById('OS').style.fontSize = parseInt(14 * size);
  document.getElementById('BaseBoard').style.fontSize = parseInt(14 * size);
  document.getElementById('BIOS').style.fontSize = parseInt(14 * size);
  document.getElementById('ComputerSystem').style.fontSize = parseInt(14 * size);
  document.getElementById('Manufacturer').style.top = parseInt(4 * size);
  document.getElementById('Name').style.top = parseInt(14 * size);
  document.getElementById('Caption').style.top = parseInt(24 * size);
  document.getElementById('ProcessorId').style.top = parseInt(34 * size);
  document.getElementById('MaxClockSpeed').style.top = parseInt(44 * size);
  document.getElementById('CurrentVoltage').style.top = parseInt(54 * size);
  document.getElementById('NumberOfCores').style.top = parseInt(64 * size);
  document.getElementById('NumberOfLogicalProcessors').style.top = parseInt(74 * size);
  document.getElementById('Caption').style.left = parseInt(7 * size);
  document.getElementById('Name').style.left = parseInt(7 * size);
  document.getElementById('NumberOfCores').style.left = parseInt(7 * size);
  document.getElementById('NumberOfLogicalProcessors').style.left = parseInt(7 * size);
  document.getElementById('ProcessorId').style.left = parseInt(7 * size);
  document.getElementById('Manufacturer').style.left = parseInt(7 * size);
  document.getElementById('MaxClockSpeed').style.left = parseInt(7 * size);
  document.getElementById('CurrentVoltage').style.left = parseInt(7 * size);
}