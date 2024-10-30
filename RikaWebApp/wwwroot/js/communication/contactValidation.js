const lengthValidator = (value, minLenght, maxLength) => {
  if (value.length >= minLenght && value.length <= maxLength)
      return true

  return false
}

const emailValidator = (email) => {
  return /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(email)
}

const formErrorHandler = (e, validationResult) => {
  let spanElement = document.querySelector(`[data-valmsg-for="${e.target.name}"]`)

  if (validationResult) {
      e.target.classList.remove('input-validation-error')
      spanElement.classList.remove('field-validation-error')
      spanElement.classList.add('field-validation-valid')
      spanElement.innerHTML = ''
  }
  else {
      if (e.target.name != "AccountDetails.BasicInfoForm.PhoneNumber" && e.target.name != "AccountDetails.BasicInfoForm.Bio") {
          e.target.classList.add('input-validation-error')
          spanElement.classList.add('field-validation-error')
          spanElement.classList.remove('field-validation-valid')
          spanElement.innerHTML = e.target.dataset.valRequired
      }
  }
}

let inputs = document.querySelectorAll('input')

let formFields = {
  fullName: false,
  email: false,
  message: false,
  checkbox: true
};



const formTextValidator = (e) => {
  formErrorHandler(e, lengthValidator(e.target.value, 2, 45))
  formFields.fullName = lengthValidator(e.target.value, 2, 25)

  validateForm();
}

const formTextAreaValidator = (e) => {
  formErrorHandler(e, lengthValidator(e.target.value, 7, 300))
  formFields.message = lengthValidator(e.target.value, 7, 300)

  validateForm();
}

const formEmailValidator = (e) => {
  formErrorHandler(e, emailValidator(e.target.value))

  formFields.email = emailValidator(e.target.value)
  validateForm();
}

function validateForm() {
  let fieldValues = []

  const submitBtn = document.querySelector('#contactSubmitBtn')

  for (let fieldKey in formFields) {
      let fieldValue = formFields[fieldKey];
      fieldValues.push(fieldValue);   
  }

  if (fieldValues.includes(false)) {
      submitBtn.disabled = true
      submitBtn.classList.add('btn-disabled')
      return false
  } else {
      submitBtn.disabled = false
      submitBtn.classList.remove('btn-disabled')
      return true
  }

}

/* 
Adding event-listeners for all inputs on the site
*/
inputs.forEach(input => {

  if (input.dataset.val === 'true') {

      input.addEventListener('input', (e) => {
          if (e.target.type == "email") {
              formEmailValidator(e)
          } else if (e.target.type == "checkbox") {
              formErrorHandler(e, e.target.checked)
          }
          else {
              formTextValidator(e)
          }
      })
  }
})

document.getElementById("contactMessage").addEventListener('input', (e) => {
  formTextAreaValidator(e)
})

/*
Validate the form on page load
*/
document.addEventListener("DOMContentLoaded", () => {
  validateForm();
})
