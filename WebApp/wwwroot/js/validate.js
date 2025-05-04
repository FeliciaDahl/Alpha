



window.validateField = (field) => {
    const errorSpan = document.querySelector(`span[data-valmsg-for='${field.name}']`);

    if (!errorSpan) return; 

    let errorMessage = "";
    const value = field.value.trim();

    if (field.hasAttribute("data-val-required") && value === "")
        errorMessage = field.getAttribute("data-val-required");

    if (field.hasAttribute("data-val-regex") && value !== "") {
        const pattern = new RegExp(field.getAttribute("data-val-regex-pattern"));
        if (!pattern.test(value))
            errorMessage = field.getAttribute("data-val-regex");
    }

    if (errorMessage) {
        field.classList.add("input-validation-error");
        errorSpan.classList.remove("field-validation-valid");
        errorSpan.classList.add("field-validation-error");
        errorSpan.textContent = errorMessage;
    } else {
        field.classList.remove("input-validation-error");
        errorSpan.classList.remove("field-validation-error");
        errorSpan.classList.add("field-validation-valid");
        errorSpan.textContent = "";
    }
};


window.initValidation = () => {
    const forms = document.querySelectorAll('form');

    forms.forEach(form => {
        const fields = form.querySelectorAll("input[data-val='true']");

        fields.forEach(field => {
            field.addEventListener("input", function () {
                window.validateField(field);
            });
        });
    });
    };

document.addEventListener('DOMContentLoaded', function () {
    window.initValidation();

});







