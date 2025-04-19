(()=>{"use strict";class e{static FullDateTimeFormat={timeZone:"Europe/Amsterdam",dateStyle:"full",timeStyle:"medium"};static Delete(t,a,n,o,r){bootbox.confirm({message:`<div class="alert alert-warning"><p class="fw-bold">${a}</p><p><strong>LET OP!</strong> ${n}.</p></div>`,buttons:{confirm:{label:"Ja",className:"btn-danger"},cancel:{label:"Nee",className:"btn-secondary"}},callback:async a=>{if(!a)return;e.ShowToastr();const n=t.dataset.url;if(!(await fetch(n,{method:"DELETE",cache:"no-cache",redirect:"follow"})).ok)return e.RemoveToastr(),void e.ToastrError("Error",o);window.location.assign(r),window.location.reload()}})}static FloorDescriptionChange(){this.closest("form").elements.Amount.value=this.options[this.selectedIndex].dataset.price}static async GetBlobSasUrl(e,t,a){const n=new URL(t,e);n.searchParams.append("extension",a);const o=await fetch(n.href,{cache:"no-cache"});return await o.json()}static GetHtmlInputElement(e){return document.querySelector(e)}static GetHtmlInputElementById(e){return document.getElementById(e)}static GetHtmlSelectElementById(e){return document.getElementById(e)}static GetSelectOption(t){const a=e.GetHtmlSelectElementById(t);return a.options[a.selectedIndex]}static InitInvoiceOrQuotationFormLine(e){$(e).validate({messages:{Amount:"Bedrag is vereist",Description:"Omschrijving is vereist",Quantity:"Aantal is vereist"},rules:{Amount:{required:!0,number:!0},Description:{required:!0},Quantity:{required:!0,number:!0}}})}static RemoveToastr(){toastr.clear()}static SetHtmlTextContent(e,t){document.querySelector(e).textContent=t}static ShowModalLine(e,t){const a=$(e),n=document.getElementById(t);n.reset(),n.elements.namedItem("Id").value="00000000-0000-0000-0000-000000000000",a.on("show.bs.modal",(function(){setTimeout((()=>{n.elements.namedItem("Quantity").focus()}),250)})),a.modal("show")}static ShowToastr(e="Please wait..",t="LOADING"){toastr.info(e,t,{closeButton:!1,extendedTimeOut:0,hideDuration:10,hideMethod:"slideUp",newestOnTop:!0,positionClass:"toast-top-center",preventDuplicates:!0,showDuration:10,showMethod:"slideDown",tapToDismiss:!1,timeOut:0})}static ToastrError(e,t){toastr.error(t,e,{hideMethod:"slideUp",hideDuration:10,newestOnTop:!0,positionClass:"toast-top-center",preventDuplicates:!0,showDuration:10,showMethod:"slideDown"})}static UniqueId(){return Math.random().toString(36).substring(2,10)}static UploadNewBlob(e,t,a){return fetch(t,{body:a,cache:"no-cache",headers:{"x-ms-content-type":e,"x-ms-blob-type":"BlockBlob","x-ms-version":"2020-10-02"},method:"PUT"})}}const t=e;class a{static abortController=null;static async AddressCheck(){a.abortController&&(a.abortController.abort(),console.error("aborting previous request")),a.abortController=new AbortController;const e=a.abortController.signal,n=document.getElementById("form-client").elements,o=n.Zipcode,r=o.value.trim(),s=+n.Number.value,i=n.Address,l=n.City,d=n.Region;i.value="",l.value="",d.value="";const c=t.GetHtmlInputElementById("Address"),m=t.GetHtmlInputElementById("Region");if(c.setAttribute("readonly","readonly"),l.setAttribute("readonly","readonly"),m.setAttribute("readonly","readonly"),!n.Country.value.match(/Nederland/i))return c.removeAttribute("readonly"),l.removeAttribute("readonly"),void m.removeAttribute("readonly");const u=new URL("/bzk/locatieserver/search/v3_1/free","https://api.pdok.nl");u.searchParams.append("q",`postcode:${r}`),u.searchParams.append("fq",`huisnummer:${s}`),u.searchParams.append("fl","woonplaatsnaam,straatnaam,huisnummer,postcode,provincienaam"),u.searchParams.append("rows","1");const p=await fetch(u.toString(),{method:"GET",cache:"no-cache",redirect:"follow",signal:e}),h=await p.json();if(p.ok&&h?.response?.numFound){const e=h.response.docs[0];return i.value=e.straatnaam,l.value=e.woonplaatsnaam,d.value=e.provincienaam,void(o.value=e.postcode)}a.abortController=null}static DeleteAppointment(){t.Delete(this,"Weet u zeker dat u deze afspraak wilt verwijderen?","dit kan niet ongedaan worden gemaakt","Er is een fout opgetreden bij het verwijderen van een afspraak.",`${window.location.pathname}#nav-appointments`)}static async DeleteAttachment(){bootbox.confirm({message:'<div class="alert alert-warning"><p class="fw-bold">Weet u zeker dat u deze bijlage wilt verwijderen?</p><p><strong>LET OP!</strong> dit kan niet ongedaan worden gemaakt.</p></div>',buttons:{confirm:{label:"Ja",className:"btn-danger"},cancel:{label:"Nee",className:"btn-secondary"}},callback:async e=>{if(!e)return;t.ShowToastr();const a=this.dataset.url;if(!(await fetch(a,{method:"DELETE",cache:"no-cache"})).ok)return t.RemoveToastr(),void t.ToastrError("Error","Er is een fout opgetreden bij het verwijderen van een attachment.");window.location.href=`${window.location.pathname}#nav-attachments`,window.location.reload()}})}static async GetSasBlobUrl(e){const t=new URL("/Attachment/GetSasBlobUrl",window.location.origin);t.searchParams.append("extension",e);const a=await fetch(t.href,{cache:"no-cache",credentials:"include",method:"GET"});return await a.json()}static InitDropZone(){const e=document.getElementById("dropzone");e.addEventListener("dragover",(e=>{e.preventDefault()})),e.addEventListener("dragleave",(e=>{e.preventDefault()})),e.addEventListener("drop",(async e=>{e.preventDefault();const n=e.dataTransfer.files,o=Array.from(n).map((e=>e.name)).filter(Boolean).join("<br>");let r={};const s=Array.from(n).map((e=>{const a=t.UniqueId();return r[a]=e,`<div data-pair="${a}">${e.name}</div><div>${e.size}</div><div>${e.type}</div><div><div class="progress" role="progressbar" aria-valuemin="0" aria-valuemax="100"><div class="progress-bar" style="width: 0"></div></div></div>`})).filter(Boolean).join("");bootbox.confirm({message:`<div class="alert alert-warning"><p class="fw-bold">Weet u zeker dat u deze bijlage(s) wilt toevoegen?</p>${o}</div>`,buttons:{confirm:{label:"Ja",className:"btn-danger"},cancel:{label:"Nee",className:"btn-secondary"}},callback:async e=>{if(!e)return;document.getElementById("dropped").insertAdjacentHTML("beforeend",s),t.ShowToastr();let n=[];Object.getOwnPropertyNames(r).forEach((e=>n.push(a.UploadAttachment(e,r[e])))),await Promise.all(n),window.location.assign(`${window.location.pathname}#nav-attachments`),window.location.reload()}})}))}static Initialize(){document.getElementById("Country").addEventListener("change",a.AddressCheck,!1),document.getElementById("Zipcode").addEventListener("blur",a.AddressCheck,!1),document.getElementById("Number").addEventListener("blur",a.AddressCheck,!1),a.InitForm(),a.InitAppointmentTable(),a.InitAttachmentTable(),a.InitDropZone(),document.querySelectorAll("#table-attachments button.btn-danger").forEach((e=>e.addEventListener("click",a.DeleteAttachment))),document.querySelectorAll("#table-appointments button.btn-danger").forEach((e=>e.addEventListener("click",a.DeleteAppointment)));const e=window.location.hash;if(e){const t=document.querySelector(`button[data-bs-target="${e}"]`);t&&t.dispatchEvent(new Event("click"))}}static InitForm(){$("#form-client").validate({messages:{BoatBrand:"Merk is vereist",BoatType:"Type is vereist",Email:"Email is vereist",LastName:"Achternaam is vereist",Address:"Adres is vereist",City:"Plaats is vereist",Region:"Provincie is vereist",Zipcode:"Postcode is vereist",Number:"Huisnummer is vereist"},rules:{BoatBrand:"required",BoatType:"required",Email:"required",LastName:"required",Address:"required",City:"required",Region:"required",Zipcode:"required",Number:"required"},submitHandler:async function(e,a){a.preventDefault(),t.ShowToastr();const n=new FormData(e),o=new URL(e.action,window.location.origin);if(!(await fetch(o.href,{body:n,method:"POST"})).ok)return t.RemoveToastr(),void t.ToastrError("Error","Er is een fout opgetreden bij het opslaan van de order.");window.location.href="/"}})}static InitAppointmentTable(){$("#table-appointments").DataTable({autoWidth:!1,columnDefs:[{targets:[0,7],className:"text-center",orderable:!1,searchable:!1,width:"2rem"}],deferRender:!0,order:[[1,"asc"]]})}static InitAttachmentTable(){$("#table-attachments").DataTable({autoWidth:!1,columnDefs:[{targets:[0,5],className:"text-center",orderable:!1,searchable:!1,width:"2rem"},{targets:[2],type:"num"}],language:{url:"https://cdn.datatables.net/plug-ins/1.10.25/i18n/Dutch.json"},order:[[1,"asc"]],paging:!1})}static SaveAttachment(e,a,n,o){const r=t.GetHtmlInputElementById("Id").value,s=new URL("/Attachment/Add",window.location.origin);return fetch(s.toString(),{body:JSON.stringify({ClientId:r,BlobUri:e,FileName:a,ContentType:n,Size:o}),headers:{"Content-Type":"application/json"},credentials:"include",method:"POST"})}static UploadAttachment(e,n){return new Promise((async(e,o)=>{const r=await a.GetSasBlobUrl(n.name.split(".").pop()),s=await n.arrayBuffer();await t.UploadNewBlob(n.type,r,s);const i=new URL(r);await a.SaveAttachment(new URL(i.pathname,i.origin).toString(),n.name,n.type,n.size),e()}))}}document.addEventListener("DOMContentLoaded",a.Initialize)})();