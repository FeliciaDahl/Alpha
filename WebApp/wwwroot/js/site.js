document.addEventListener('DOMContentLoaded', () => {

    //OpenCloseModals();
    //EditClient();
    //EditProject();
    //initDeleteModals();
    
    uploadImage();
    uploadEditImage();
    initForms();    


    /*Handle Form Submisson*/
    function initForms() {
        const forms = document.querySelectorAll('form')
        forms.forEach(form => {
            form.addEventListener("submit", async (e) => {
                e.preventDefault()

                clearErrorMessage(form)

                const formData = new FormData(form)


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




    //let hasUploadedImage = false;

    ///*Open Modal*/
    // function OpenCloseModals() { 
    //const modalButtons = document.querySelectorAll('[data-modal="true"]')
    //modalButtons.forEach(button => {
    //    button.addEventListener('click', () => {
    //        const modalTarget = button.getAttribute('data-target')
    //        const modal = document.querySelector(modalTarget)

    //        if (modal)
    //            modal.style.display = 'flex';
    //    })
    //})

    ///*Close Modal*/
    //const closeButtons = document.querySelectorAll('[data-close="true"]')
    //closeButtons.forEach(button => {
    //    button.addEventListener('click', () => {
    //        const modal = button.closest('.modal')
           
    //        if (modal) {
    //            modal.style.display = 'none';
    //            modal.querySelectorAll('form').forEach(f => f.reset());

    //        }

    //    })
    //})
    //}

    /*   Clients EditModal  */
    //function EditClient() {
    //    const editButtons = document.querySelectorAll('.btn-edit-client');
    //    editButtons.forEach(button => {
    //        button.addEventListener('click', async function () {
    //            const clientId = this.getAttribute('data-id');
    //            if (!clientId) return;

    //            try {
    //                const res = await fetch(`/Client/EditClient?id=${clientId}`);
    //                if (res.ok) {
    //                    const client = await res.json();

                      
    //                    document.querySelector('#clientId').value = client.id;
    //                    document.querySelector('#clientName').value = client.clientName;
    //                    document.querySelector('#clientContact').value = client.contactPerson;
    //                    document.querySelector('#clientEmail').value = client.email;
    //                    document.querySelector('#clientLocation').value = client.location;
    //                    document.querySelector('#clientPhone').value = client.phone;
                        
    //                    previewExitingImage('edit', client.imagePath);
                    
             
    //                }
    //                else {
    //                    console.error('Could not load data');
    //                }
    //            } catch (error) {
    //                console.error('Error:', error);
    //            }
    //        });
    //    });
    //}


    /*   Project EditModal  */
    //function EditProject() { 
    // const editButtons = document.querySelectorAll('.btn-edit-project');
    // editButtons.forEach(button => {
    //     button.addEventListener('click', async function () {
    //         const projectId = this.getAttribute('data-id');
    //         if (!projectId) return;

    //         try {
    //             const res = await fetch(`/Project/EditProject?id=${projectId}`);
    //             if (res.ok) {
    //                 const project = await res.json();

    //                 document.querySelector('#projectId').value = project.id;
    //                 document.querySelector('#projectTitle').value = project.title;
    //                 document.querySelector('#projectClientId').value = project.clientId;
    //                 document.querySelector('#projectDescription').value = project.description;
    //                 document.querySelector('#projectStart').value = project.startDate;
    //                 document.querySelector('#projectEnd').value = project.endDate;
    //                 document.querySelector('#projectBudget').value = project.budget;

    //             }
    //             else {
    //                 console.error('Could not load data');
    //             }
    //         }
    //         catch (error) {
    //             console.error('Error:', error);
    //         }
    //     });
    // });
    //}
    


    /*Delete Modal*/
    //function deleteModal(deleteBtnClass, modalId, deleteUrlBuilder) {

    //    const modal = document.querySelector(modalId);
    //    if (!modal) return;

    //    const confirmBtn = modal.querySelector('.confirm-delete');
    //    const cancelBtn = modal.querySelector('.cancel-delete');
    //    const imageFile = document.querySelector('.image');
    //    let currentId = null;

    //    document.querySelectorAll(deleteBtnClass).forEach(button => {
    //        button.addEventListener('click', () => {
    //            currentId = button.getAttribute('data-id');
    //            modal.style.display = 'flex';
    //        });
    //    });

    //    if (cancelBtn) {
    //        cancelBtn.addEventListener('click', () => modal.style.display = 'none');
    //    }

    //    if (confirmBtn) {
    //        confirmBtn.addEventListener('click', async () => {
    //            if (!currentId) return;


    //            const formData = new FormData();
    //            formData.append('id', currentId);  
    //            formData.append('image', imageFile);  

    //            try {

    //                const res = await fetch(deleteUrlBuilder(currentId), {
    //                    method: 'POST',
    //                    body: formData  
    //                });

    //                if (res.ok) {
    //                    modal.style.display = 'none';
    //                    window.location.reload();
    //                } else {
    //                    const data = await res.json();
    //                    modal.querySelector('.delete-alert').classList.remove('hide');
    //                    modal.querySelector('.delete-error-message').innerText = data.error || 'Could not delete client. Make sure its not connected to a existing project';
                        
    //                }
    //            } catch (error) {
    //                console.error('Error:', error);
    //            }
    //        });
    //    }
    //}

    //function initDeleteModals() {
    //    deleteModal('.btn-delete-client', '#deleteClientModal', (id) => `/Client/DeleteClient/${id}`);
    //    deleteModal('.btn-delete-project', '#deleteProjectModal', (id) => `/Project/DeleteProject/${id}`);
    //}

    //Nu blir det rätt förutom att bilden följer med..
    
  
    /*Image upload preview*/
    //function uploadEditImage() {
    //    const uploadTrigger = document.getElementById('edit-upload-trigger')
    //    const fileInput = document.getElementById('edit-file-upload')
    //    const imagePreview = document.getElementById('edit-image-preview')
    //    const previewContainer = document.getElementById('edit-image-container')
    //    const uploadIcon = document.getElementById('edit-preview-camera')
    //    const changeIcon = document.getElementById('edit-preview-pencil')

    //    if (!uploadTrigger || !fileInput) console.log('not found');

    //    uploadTrigger.addEventListener('click', function () {
    //        fileInput.click()
    //    })
      

    //    fileInput.addEventListener('change', function (e) {
    //        const file = e.target.files[0]

    //        if (file && file.type.startsWith('image/')) {
    //            const reader = new FileReader()
    //            reader.onload = (e) => {
    //                imagePreview.src = e.target.result
    //                imagePreview.classList.remove('hide')

    //                uploadIcon.classList.add('hide')
    //                changeIcon.classList.remove('hide')
    //                previewContainer.classList.add('image-change')

    //                hasUploadedImage = true
    //            }

    //            reader.readAsDataURL(file)
    //        }
    //    })

    //}

    //function previewExitingImage(modal, imagePath) {
    //    if (hasUploadedImage) return

    //    const imagePreview = document.getElementById(`${modal}-image-preview`);
    //    const uploadIcon = document.getElementById(`${modal}-preview-camera`);
    //    const changeIcon = document.getElementById(`${modal}-preview-pencil`);
    //    const previewContainer = document.getElementById(`${modal}-image-container`);

    //    if (imagePath) {
    //        imagePreview.src = '/' + imagePath.replace(/\\/g, '/');
    //        imagePreview.classList.remove('hide');
    //        uploadIcon.classList.remove('hide');
    //        changeIcon.classList.add('hide');
    //        previewContainer.classList.add('image-change');
    //    }
    //    else {
           
    //        uploadIcon.classList.add('hide');
    //        changeIcon.classList.remove('hide');
    //        previewContainer.classList.remove('image-change');
    //    }
    //}



    /*Handle Dropdown*/
    const dropdowns = document.querySelectorAll('[data-type="dropdown"]')
    document.addEventListener('click', function (e) {

        let clickedOnDropdown = false

        dropdowns.forEach(dropdownBtn => {
            const targetId = dropdownBtn.getAttribute('data-target')
            const targetElement = document.querySelector(targetId)


            if (dropdownBtn.contains(e.target)) {
                clickedOnDropdown = true

                document.querySelectorAll('.dropdown-show').forEach(openDropdown => {
                    if (openDropdown !== targetElement) {
                        openDropdown.classList.remove('dropdown-show')
                    }
                })
                targetElement?.classList.toggle('dropdown-show')
            }
            else if (targetElement && targetElement.contains(e.target)) {
                clickedOnDropdown = true
            }
        })

        if (!clickedOnDropdown) {
            document.querySelectorAll('.dropdown-show').forEach(openDropdown => {
                openDropdown.classList.remove('dropdown-show')
            })

        }
    })
})


//function previewExitingImage(imagePath) {

//    const imagePreview = document.getElementById('image-preview');
//    const uploadIcon = document.getElementById('preview-camera');
//    const changeIcon = document.getElementById('preview-pencil');
//    const previewContainer = document.getElementById('image-container');

//    if (imagePath) {
//        imagePreview.src = '/' + imagePath.replace(/\\/g, '/');
//        imagePreview.classList.remove('hide');
//        uploadIcon.classList.add('hide');
//        changeIcon.classList.remove('hide');
//        previewContainer.classList.add('image-change');
//    }
//    else {
//        imagePreview.classList.add('hide');
//        uploadIcon.classList.remove('hide');
//        changeIcon.classList.add('hide');
//        previewContainer.classList.remove('image-change');
//    }
//}


function uploadImage() {

    const uploadTrigger = document.getElementById('upload-trigger')
    const fileInput = document.getElementById('file-upload')
    const imagePreview = document.getElementById('image-preview')
    const previewContainer = document.getElementById('image-container')
    const uploadIcon = document.getElementById('preview-camera')
    const changeIcon = document.getElementById('preview-pencil')

    if (!uploadTrigger || !fileInput) return;

    uploadTrigger.addEventListener('click', function () {
        fileInput.click()
    })


    fileInput.addEventListener('change', function (e) {
        const file = e.target.files[0]

        if (file && file.type.startsWith('image/')) {
            const reader = new FileReader()
            reader.onload = (e) => {
                imagePreview.src = e.target.result
                imagePreview.classList.remove('hide')

                uploadIcon.classList.add('hide')
                changeIcon.classList.remove('hide')
                previewContainer.classList.add('image-change')
            }

            reader.readAsDataURL(file)
        }
    })

}

//function previewExitingImage(imagePath) {

//    const imagePreview = document.getElementById('image-preview');
//    const uploadIcon = document.getElementById('preview-camera');
//    const changeIcon = document.getElementById('preview-pencil');
//    const previewContainer = document.getElementById('image-container');

//    if (imagePath) {
//        imagePreview.src = '/' + imagePath.replace(/\\/g, '/');
//        imagePreview.classList.remove('hide');
//        uploadIcon.classList.add('hide');
//        changeIcon.classList.remove('hide');
//        previewContainer.classList.add('image-change');
//    }
//    else {
//        imagePreview.classList.add('hide');
//        uploadIcon.classList.remove('hide');
//        changeIcon.classList.add('hide');
//        previewContainer.classList.remove('image-change');
//    }
//}
