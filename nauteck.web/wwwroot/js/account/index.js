(()=>{"use strict";class e{static FullDateTimeFormat={timeZone:"Europe/Amsterdam",dateStyle:"full",timeStyle:"medium"};static Delete(t,o,n,a,r){bootbox.confirm({message:`<div class="alert alert-warning"><p class="fw-bold">${o}</p><p><strong>LET OP!</strong> ${n}.</p></div>`,buttons:{confirm:{label:"Ja",className:"btn-danger"},cancel:{label:"Nee",className:"btn-secondary"}},callback:async o=>{if(!o)return;e.ShowToastr();const n=t.dataset.url;if(!(await fetch(n,{method:"DELETE",cache:"no-cache",redirect:"follow"})).ok)return e.RemoveToastr(),void e.ToastrError("Error",a);window.location.replace(r)}})}static async GetBlobSasUrl(e,t,o){const n=new URL(t,e);n.searchParams.append("extension",o);const a=await fetch(n.href,{cache:"no-cache"});return await a.json()}static GetHtmlInputElement(e){return document.querySelector(e)}static GetHtmlInputElementById(e){return document.getElementById(e)}static GetHtmlSelectElement(e){return document.querySelector(e)}static GetHtmlSelectElementById(e){return document.getElementById(e)}static GetSelectOption(t){const o=e.GetHtmlSelectElementById(t);return o.options[o.selectedIndex]}static RemoveToastr(){toastr.clear()}static SetHtmlTextContent(e,t){document.querySelector(e).textContent=t}static ShowToastr(e="Please wait..",t="LOADING"){toastr.info(e,t,{closeButton:!1,extendedTimeOut:0,hideDuration:10,hideMethod:"slideUp",newestOnTop:!0,positionClass:"toast-top-center",preventDuplicates:!0,showDuration:10,showMethod:"slideDown",tapToDismiss:!1,timeOut:0})}static ToastrError(e,t){toastr.error(t,e,{hideMethod:"slideUp",hideDuration:10,newestOnTop:!0,positionClass:"toast-top-center",preventDuplicates:!0,showDuration:10,showMethod:"slideDown"})}static UniqueId(){return Math.random().toString(36).substring(2,10)}static UploadNewBlob(e,t,o){return fetch(t,{body:o,cache:"no-cache",headers:{"x-ms-content-type":e,"x-ms-blob-type":"BlockBlob","x-ms-version":"2020-10-02"},method:"PUT"})}}const t=e;class o{static async Delete(){t.Delete(this,"Weet u zeker dat u deze gebruiker wilt verwijderen?","dit kan niet ongedaan worden gemaakt.","Er is een fout opgetreden bij het verwijderen van een order.",window.location.pathname)}static Initialize(){document.querySelectorAll('button[data-type="delete"]').forEach((e=>e.addEventListener("click",o.Delete)))}}document.addEventListener("DOMContentLoaded",o.Initialize)})();