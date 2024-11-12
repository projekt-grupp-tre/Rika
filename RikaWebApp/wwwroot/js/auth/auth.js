
(() => {
  
    if (typeof window.currentPath === 'undefined') {
        window.currentPath = window.location.pathname;
    }

    
    if (typeof window.allowedPaths === 'undefined') {
        window.allowedPaths = ["/SignUp/SignUpView", "/SignIn/SignInView"];
    }

    
    if (window.allowedPaths.includes(window.currentPath)) {

       
        if (typeof window.formErrorHandler !== 'function') {
            window.formErrorHandler = (element, validationResult) => {
                let spanElement = document.querySelector(`[data-valmsg-for="${element.name}"]`);

                if (validationResult) {
                    element.classList.remove('input-valitation-error');
                    spanElement.classList.remove('field-validation-error');
                    spanElement.classList.add('field-validation-valid');
                    spanElement.innerHTML = '';
                } else {
                    element.classList.add('input-valitation-error');
                    spanElement.classList.add('field-validation-error');
                    spanElement.classList.remove('field-validation-valid');
                    spanElement.innerHTML = element.dataset.valRequired;
                }
            };

            const compareValidator = (element, compareValue) => element.value === compareValue;

            const textValidator = (element, minLength = 2) => {
                window.formErrorHandler(element, element.value.length >= minLength);
            };

            const emailValidator = (element) => {
                const regEx = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
                window.formErrorHandler(element, regEx.test(element.value));
            };

            const passValidator = (element) => {
                if (element.dataset.valEqualToOther !== undefined) {
                    let password = document.getElementsByName(element.dataset.valEqualToOther.replace('*', 'Form'))[0].value;
                    window.formErrorHandler(element, element.value === password);
                } else {
                    const regEx = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$/;
                    window.formErrorHandler(element, regEx.test(element.value));
                }
            };

            const phoneValidator = (element) => {
                const regEx = /^\d{10}$/;
                window.formErrorHandler(element, regEx.test(element.value));
            };

            const checkboxValidator = (element) => {
                window.formErrorHandler(element, element.checked);
            };

            let forms = document.querySelectorAll('form');
            let inputs = document.querySelectorAll('input');

            inputs.forEach(input => {
                if (input.dataset.val === 'true') {
                    if (input.type === 'checkbox') {
                        input.addEventListener('change', (e) => {
                            checkboxValidator(e.target);
                        });
                    } else {
                        input.addEventListener('keyup', (e) => {
                            switch (e.target.type) {
                                case 'text':
                                    textValidator(e.target);
                                    break;
                                case 'email':
                                    emailValidator(e.target);
                                    break;
                                case 'password':
                                    passValidator(e.target);
                                    break;
                                case 'tel':
                                    phoneValidator(e.target);
                                    break;
                            }
                        });
                    }
                }
            });

            function checkFields() {
                let allFieldsFilled = true;
                inputs.forEach(input => {
                    if (!input.value) {
                        allFieldsFilled = false;
                    }
                });
            }

            inputs.forEach(input => {
                input.addEventListener('keyup', checkFields);
            });

            checkFields();
        }
    }
})();
