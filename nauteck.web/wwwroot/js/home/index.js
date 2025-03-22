(()=>{"use strict";class e{static FullDateTimeFormat={timeZone:"Europe/Amsterdam",dateStyle:"full",timeStyle:"medium"};static Delete(t,a,n,o,s){bootbox.confirm({message:`<div class="alert alert-warning"><p class="fw-bold">${a}</p><p><strong>LET OP!</strong> ${n}.</p></div>`,buttons:{confirm:{label:"Ja",className:"btn-danger"},cancel:{label:"Nee",className:"btn-secondary"}},callback:async a=>{if(!a)return;e.ShowToastr();const n=t.dataset.url;if(!(await fetch(n,{method:"DELETE",cache:"no-cache",redirect:"follow"})).ok)return e.RemoveToastr(),void e.ToastrError("Error",o);window.location.assign(s),window.location.reload()}})}static async GetBlobSasUrl(e,t,a){const n=new URL(t,e);n.searchParams.append("extension",a);const o=await fetch(n.href,{cache:"no-cache"});return await o.json()}static GetHtmlInputElement(e){return document.querySelector(e)}static GetHtmlInputElementById(e){return document.getElementById(e)}static GetHtmlSelectElement(e){return document.querySelector(e)}static GetHtmlSelectElementById(e){return document.getElementById(e)}static GetSelectOption(t){const a=e.GetHtmlSelectElementById(t);return a.options[a.selectedIndex]}static RemoveToastr(){toastr.clear()}static SetHtmlTextContent(e,t){document.querySelector(e).textContent=t}static ShowToastr(e="Please wait..",t="LOADING"){toastr.info(e,t,{closeButton:!1,extendedTimeOut:0,hideDuration:10,hideMethod:"slideUp",newestOnTop:!0,positionClass:"toast-top-center",preventDuplicates:!0,showDuration:10,showMethod:"slideDown",tapToDismiss:!1,timeOut:0})}static ToastrError(e,t){toastr.error(t,e,{hideMethod:"slideUp",hideDuration:10,newestOnTop:!0,positionClass:"toast-top-center",preventDuplicates:!0,showDuration:10,showMethod:"slideDown"})}static UniqueId(){return Math.random().toString(36).substring(2,10)}static UploadNewBlob(e,t,a){return fetch(t,{body:a,cache:"no-cache",headers:{"x-ms-content-type":e,"x-ms-blob-type":"BlockBlob","x-ms-version":"2020-10-02"},method:"PUT"})}}const t=e;class a{static async Delete(){t.Delete(this,"Weet u zeker dat u deze klant wilt verwijderen?","dit kan niet ongedaan worden gemaakt en alle gekoppelde items en documenten worden tevens verwijderd.","Er is een fout opgetreden bij het verwijderen van een klant.",window.location.pathname)}static InitDatatable(){$("#table-orders").DataTable({autoWidth:!1,columnDefs:[{targets:[0,10],className:"text-center",orderable:!1,searchable:!1,width:"2rem"}],order:[[1,"asc"],[3,"asc"],[4,"asc"]],deferRender:!0,language:{url:"https://cdn.datatables.net/plug-ins/1.10.25/i18n/Dutch.json"},lengthMenu:[10,25,50,100],pageLength:10,processing:!0,stateSave:!0})}static Initialize(){document.querySelectorAll("button.btn-danger").forEach((e=>e.addEventListener("click",a.Delete))),a.InitDatatable()}}document.addEventListener("DOMContentLoaded",a.Initialize)})();