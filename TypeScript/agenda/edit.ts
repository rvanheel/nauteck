class AgendaEdit{
    static Initialize(){
        $("#form-appointment").validate({
            messages: {
                "Date": "Datum is vereist",
                "Status": "Status is vereist",
                "Title": "Titel is vereist"
            },
            rules:{
                "Date": "required",
                "Status": "required",
                "Title": "required"
            }
        })
    }
}
document.addEventListener("DOMContentLoaded", AgendaEdit.Initialize);