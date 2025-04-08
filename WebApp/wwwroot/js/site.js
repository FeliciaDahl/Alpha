document.addEventListener('DOMContentLoaded', () => {


    /*Open Modal*/
    const modalButtons = document.querySelectorAll('[data-modal="true"]')
    modalButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modalTarget = button.getAttribute('data-target')
            const modal = document.querySelector(modalTarget)

            if (modal)
                modal.style.display = 'flex';
        })
    })

    /*Close Modal*/
    const closeButtons = document.querySelectorAll('[data-close="true"]')
    closeButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modal = button.closest('.modal')
            if (modal) {
                modal.style.display = 'none';

                modal.querySelectorAll('form').forEach(form => {
                    form.reset();
                })
            }
                
        })
    })

    /*  Open Clients EditModal  */
    const editButtons = document.querySelectorAll('.btn-edit');
    editButtons.forEach(button => {
        button.addEventListener('click', async function () {
            const clientId = this.getAttribute('data-id');
            if (!clientId) return;

            try {
                const res = await fetch(`/Clients/EditClient?id=${clientId}`);
                if (res.ok) {
                    const client = await res.json();

                    document.querySelector('#clientId').value = client.id;
                    document.querySelector('#clientName').value = client.clientName;
                    document.querySelector('#clientContact').value = client.contactPerson;
                    document.querySelector('#clientEmail').value = client.email;
                    document.querySelector('#clientLocation').value = client.location;
                    document.querySelector('#clientPhone').value = client.phone;

                  
                    const modal = document.querySelector('#editClientModal');
                    if (modal)
                        modal.style.display = 'flex';
                } else {
                    console.error('Could not load data');
                }
            } catch (error) {
                console.error('Error:', error);
            }
        });
    });



    /*Handle Form Submisson*/

    const forms = document.querySelectorAll('form')
    forms.forEach(form => {
    form.addEventListener("submit", async (e) => {
        e.preventDefault()

        clearErrorMessage(form)

        const formData = new FormData(form)

        const method = form.getAttribute("data-method") || "post";

        try {
            const res = await fetch(form.action, {
                method: method.toUpperCase(),
                body: formData
            });

            if (res.ok) {
                const modal = form.closest('.modal')
                if (modal) {
                    modal.style.display = 'none';

                    window.location.reload()
                }
            }

            if (res.status === 400) {
                const data = await res.json()
                if (data.errors) {
                    Object.keys(data.errors).forEach(key => {
                        const input = form.querySelector(`[name="${key}"]`)

                        if (input) {
                            input.classList.add('input-validation-error')

                        }
                        const span = form.querySelector(`[data-valmsg-for="${key}"]`)

                        if (span) {
                            span.innerText = data.errors[key].join('\n')
                            span.classList.add('field-validation-error')
                        }

                    })
                }
            }
        }
        catch (error) {
            console.error('Error:', error)
        }
    })
    })
})

function clearErrorMessage(form) {
    form.querySelectorAll('[data-val="true"]').forEach(input => {
        input.classList.remove('input-validation-error')
    })
    form.querySelectorAll('[data-valmsg-for]').forEach(span => {
        span.innerText = ''
        span.classList.remove('field-validation-error')
    })
}



