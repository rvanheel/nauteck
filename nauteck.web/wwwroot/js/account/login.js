(()=>{"use strict";class e{static FullDateTimeFormat={timeZone:"Europe/Amsterdam",dateStyle:"full",timeStyle:"medium"};static async GetBlobSasUrl(e,t,s){const o=new URL(t,e);o.searchParams.append("extension",s);const r=await fetch(o.href,{cache:"no-cache"});return await r.json()}static GetHtmlInputElement(e){return document.querySelector(e)}static GetHtmlInputElementById(e){return document.getElementById(e)}static GetHtmlSelectElement(e){return document.querySelector(e)}static GetHtmlSelectElementById(e){return document.getElementById(e)}static GetSelectOption(t){const s=e.GetHtmlSelectElementById(t);return s.options[s.selectedIndex]}static RemoveToastr(){toastr.remove()}static SetHtmlTextContent(e,t){document.querySelector(e).textContent=t}static ShowToastr(e="Please wait..",t="LOADING"){toastr.info(e,t,{closeButton:!1,extendedTimeOut:0,hideDuration:10,hideMethod:"slideUp",newestOnTop:!0,positionClass:"toast-top-center",preventDuplicates:!0,showDuration:10,showMethod:"slideDown",tapToDismiss:!1,timeOut:0})}static ToastrError(e,t){toastr.error(t,e,{hideMethod:"slideUp",hideDuration:10,newestOnTop:!0,positionClass:"toast-top-center",preventDuplicates:!0,showDuration:10,showMethod:"slideDown"})}static UploadNewBlob(e,t,s){return fetch(t,{body:s,cache:"no-cache",headers:{"x-ms-content-type":e,"x-ms-blob-type":"BlockBlob","x-ms-version":"2020-10-02"},method:"PUT"})}}const t=e;document.addEventListener("DOMContentLoaded",class{static Init(){const e=localStorage.getItem("user"),s=localStorage.getItem("password");document.querySelector("input[name=User]").value=e,document.querySelector("input[name=Password]").value=s,$("#form-login").validate({messages:{Password:"Het veld Password is vereist.",User:"Het veld gebruiker is vereist."},rules:{Password:"required",User:"required"},submitHandler(e,s){document.querySelector("button[type=submit]").setAttribute("disabled","disabled"),t.ShowToastr(),localStorage.setItem("user",e.querySelector("input[name=User]").value),localStorage.setItem("password",e.querySelector("input[name=Password]").value),e.submit()}})}}.Init)})();