document.addEventListener('DOMContentLoaded', () => {

    OpenCloseModals();
    EditClient();
    initDeleteModals();
    initForms();

});


function OpenCloseModals() {

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
                modal.querySelectorAll('form').forEach(f => f.reset());
                
            }

        })
    })
}
/*   Clients EditModal  */
function EditClient() { 
    const editButtons = document.querySelectorAll('.btn-edit');
    editButtons.forEach(button => {
        button.addEventListener('click', async function () {
            const clientId = this.getAttribute('data-id');
            if (!clientId) return;

            try {
                const res = await fetch(`/Client/EditClient?id=${clientId}`);
                if (res.ok) {
                    const client = await res.json();

                    document.querySelector('#clientId').value = client.id;
                    document.querySelector('#clientName').value = client.clientName;
                    document.querySelector('#clientContact').value = client.contactPerson;
                    document.querySelector('#clientEmail').value = client.email;
                    document.querySelector('#clientLocation').value = client.location;
                    document.querySelector('#clientPhone').value = client.phone;

                  
                }
                else {
                    console.error('Could not load data');
                }
            } catch (error) {
                console.error('Error:', error);
            }
        });
    });
}

function deleteModal(deleteBtnClass, modalId, deleteUrlBuilder) {

    const modal = document.querySelector(modalId);
    if (!modal) return;

    const confirmBtn = modal.querySelector('.confirm-delete');
    const cancelBtn = modal.querySelector('.cancel-delete');
    const imageFile = document.querySelector('.image');
    let currentId = null;

    document.querySelectorAll(deleteBtnClass).forEach(button => {
        button.addEventListener('click', () => {
            currentId = button.getAttribute('data-id');
            modal.style.display = 'flex';
        });
    });

    if (cancelBtn) {
        cancelBtn.addEventListener('click', () => modal.style.display = 'none');
    }

    if (confirmBtn) {
        confirmBtn.addEventListener('click', async () => {
            if (!currentId) return;

          
            const formData = new FormData();
            formData.append('id', currentId);  
            formData.append('image', imageFile);  

      

            try {
              
                const res = await fetch(deleteUrlBuilder(currentId), {
                    method: 'POST',
                    body: formData  
                });

                if (res.ok) {
                    modal.style.display = 'none';
                    window.location.reload();
                } else {
                    const data = await res.json();
                    console.error('Delete failed:', data);
                }
            } catch (error) {
                console.error('Error:', error);
            }
        });
    }
}

function initDeleteModals() {
    deleteModal('.btn-delete-client', '#deleteClientModal', (id) => `/Client/DeleteClient/${id}`);
    deleteModal('.btn-delete-project', '#deleteProjectModal', (id) => `/Project/DeleteProject/${id}`);
}



    /*Handle Form Submisson*/
function initForms() { 
    const forms = document.querySelectorAll('form')
    forms.forEach(form => {
    form.addEventListener("submit", async (e) => {
        e.preventDefault()

        clearErrorMessage(form)

        const formData = new FormData(form)

        //const method = form.getAttribute("data-method") || "post";

        try {
            const res = await fetch(form.action, {
                method: 'post',
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
}

function clearErrorMessage(form) {
    form.querySelectorAll('[data-val="true"]').forEach(input => {
        input.classList.remove('input-validation-error')
    })
    form.querySelectorAll('[data-valmsg-for]').forEach(span => {
        span.innerText = ''
        span.classList.remove('field-validation-error')
    })
}



