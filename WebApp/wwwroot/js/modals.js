document.addEventListener('DOMContentLoaded', () => {

    OpenCloseModals();
    EditClient();
    EditProject();
    initDeleteModals();
    uploadEditImage();


    let hasUploadedImage = false;

    /*Open Modal*/
    function OpenCloseModals() {
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

    /*Edit Client*/
    function EditClient() {
        const editButtons = document.querySelectorAll('.btn-edit-client');
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

                        previewExitingImage('edit', client.imagePath);


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

    /*Edit Project*/

    function EditProject() {
        const editButtons = document.querySelectorAll('.btn-edit-project');
        editButtons.forEach(button => {
            button.addEventListener('click', async function () {
                const projectId = this.getAttribute('data-id');
                if (!projectId) return;

                try {
                    const res = await fetch(`/Project/EditProject?id=${projectId}`);
                    if (res.ok) {
                        const project = await res.json();

                        document.querySelector('#projectId').value = project.id;
                        document.querySelector('#projectTitle').value = project.title;
                        document.querySelector('#projectClientId').value = project.clientId;
                        document.querySelector('#projectDescription').value = project.description;
                        document.querySelector('#projectStart').value = project.startDate.split('T')[0];
                        document.querySelector('#projectEnd').value = project.endDate.split('T')[0];
                        document.querySelector('#projectStatusId').value = project.statusId;
                        document.querySelector('#projectBudget').value = project.budget;

                        console.log(project.projectImagePath);

                        previewExitingImage('edit', project.projectImagePath);

                    }
                    else {
                        console.error('Could not load data');
                    }
                }
                catch (error) {
                    console.error('Error:', error);
                }
            });
        });
    }


    //Blir problematiskt att använda samma Id/namn  : const changeIcon osv, krockar med uploadEditImage.. hjälp.
    /*Preview Existing Image*/
    function previewExitingImage(modal, imagePath) {
        if (hasUploadedImage) return

        const imagePreview = document.getElementById(`${modal}-image-preview`);
        const uploadIcon = document.getElementById(`${modal}-preview-camera`);
        const changeIcon = document.getElementById(`${modal}-preview-pencil`);
        const previewContainer = document.getElementById(`${modal}-image-container`);

        if (imagePath) {
            imagePreview.src = '/' + imagePath.replace(/\\/g, '/');
            imagePreview.classList.remove('hide');
            uploadIcon.classList.add('hide');
            changeIcon.classList.remove('hide');
            previewContainer.classList.add('image-change');
        }
        else {
            imagePreview.src ='~/images/Avatar-2.svg';

            uploadIcon.classList.add('hide');
            changeIcon.classList.remove('hide');
            previewContainer.classList.remove('image-change');
        }
    }

    /*UploadEdit Image*/

    function uploadEditImage() {
        const uploadTrigger = document.getElementById('edit-upload-trigger')
        const fileInput = document.getElementById('edit-file-upload')
        const imagePreview = document.getElementById('edit-image-preview')
        const previewContainer = document.getElementById('edit-image-container')
        const uploadIcon = document.getElementById('edit-preview-camera')
        const changeIcon = document.getElementById('edit-preview-pencil')

        if (!uploadTrigger || !fileInput) console.log('not found');

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

                    hasUploadedImage = true
                }

                reader.readAsDataURL(file)
            }
        })

    }

    /*Delete*/

    function deleteModal(deleteBtnClass, modalId, deleteUrlBuilder, errorMessage) {

        const modal = document.querySelector(modalId);
        if (!modal) return;

        const confirmBtn = modal.querySelector('.confirm-delete');
        const cancelBtn = modal.querySelector('.cancel-delete');
        const imageFile = document.querySelector('.image');
        const deleteAlert = modal.querySelector('.delete-alert');
        const messageAlert = modal.querySelector('.delete-error-message');

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
                        deleteAlert.classList.remove('hide');
                        deleteAlert.style.display = 'flex';
                        messageAlert.innerText = data.error || errorMessage;

                    }
                } catch (error) {
                    console.error('Error:', error);
                }
            });
        }
    }
    function initDeleteModals() {
        deleteModal('.btn-delete-client', '#deleteClientModal', (id) => `/Client/DeleteClient/${id}`, 'Could not delete client. Make sure its not connected to a existing project');
        deleteModal('.btn-delete-project', '#deleteProjectModal', (id) => `/Project/DeleteProject/${id}`, 'Could not delete project. Try again.');
    }




})