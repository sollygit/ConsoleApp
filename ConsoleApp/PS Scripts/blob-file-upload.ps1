$env:AZCOPY_CRED_TYPE = "Anonymous";
$env:AZCOPY_USER_AGENT_PREFIX = "Microsoft Azure Storage Explorer, 1.12.0, win32, ";

azcopy.exe copy "C:\Git\QuickApp\Solution Items\GoodData.dev.csv" "http://127.0.0.1:10000/devstoreaccount1/import/GoodData.dev.csv?se=2020-04-17T06%3A28%3A46Z&sp=rwl&sv=2018-03-28&sr=c&sig=kUK6O9JJPxjCkXD3oO7ixvYd1j8h%2BkbHJAMc0o%2F5ImY%3D" --overwrite=true --follow-symlinks --recursive --from-to=LocalBlob --blob-type=Detect --put-md5;

$env:AZCOPY_CRED_TYPE = "";
$env:AZCOPY_USER_AGENT_PREFIX = "";