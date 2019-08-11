curl -u test:dupa1234 'http://127.0.0.1:8080/remote.php/dav/files/test/elo/' -X PROPFIND --data '<?xml version="1.0" encoding="UTF-8"?>
 <d:propfind xmlns:d="DAV:">
   <d:prop xmlns:oc="http://owncloud.org/ns">
     <d:getlastmodified/>
     <d:getcontentlength/>
     <d:getcontenttype/>
     <oc:permissions/>
     <d:resourcetype/>
     <d:getetag/>
   </d:prop>
 </d:propfind>' | xmllint --format -
