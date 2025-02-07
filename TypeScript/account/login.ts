import Functions from "./Functions";

class Login {
  static Init() {
    const user = localStorage.getItem("user");
    const password = localStorage.getItem("password");
    (document.querySelector("input[name=User]") as HTMLInputElement).value = user;
    (document.querySelector("input[name=Password]") as HTMLInputElement).value = password;
    $("#form-login").validate({
      messages: {
        Password: "Het veld Password is vereist.",
        User: "Het veld gebruiker is vereist."
      },
      rules: {
        Password: "required",
        User: "required"
      },
      submitHandler(form: HTMLFormElement, event?: JQueryEventObject) {
        const btn = document.querySelector("button[type=submit]");
        btn.setAttribute("disabled", "disabled");
        Functions.ShowToastr();
        localStorage.setItem("user", (form.querySelector("input[name=User]") as HTMLInputElement).value);
        localStorage.setItem("password", (form.querySelector("input[name=Password]") as HTMLInputElement).value);
        form.submit();
      }
    });
  }
}
document.addEventListener('DOMContentLoaded', Login.Init);