
/*Handle Form Submisson*/
    function initForms() {
        const forms = document.querySelectorAll('form')
        forms.forEach(form => {

            if (!form.closest('.modal')) return;

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


/*Handle Dropdown*/
function handleDropDowns() {
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
}

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

function darkModeToggle() {
    const toggle = document.getElementById("darkModelToggle");
    const prefersDark = localStorage.getItem("darkMode") === "true";

    if (prefersDark) {
        document.documentElement.setAttribute("data-theme", "dark");
        toggle.checked = true;
    }

    toggle.addEventListener("change", () => {
        if (toggle.checked) {
            document.documentElement.setAttribute("data-theme", "dark");
            localStorage.setItem("darkMode", "true");
        } else {
            document.documentElement.removeAttribute("data-theme");
            localStorage.setItem("darkMode", "false");
        }
    })
}

function updateRelativeTimes() {
    const elements = document.querySelectorAll('.notification-content .time');
    const now = new Date();

    elements.forEach(element => {
        const created = new Date(element.getAttribute('data-created'));
        const diff = now - created;
        const diffSeconds = Math.floor(diff / 1000);
        const diffMinutes = Math.floor(diffSeconds / 60);
        const diffHours = Math.floor(diffMinutes / 60);
        const diffDays = Math.floor(diffHours / 24);
        const diffWeeks = Math.floor(diffDays / 7);

        let relativeTime = '';
        if (diffMinutes < 1) {
            realtiveTime = 'Just now';
        } else if (diffMinutes < 60) {
            relativeTime = diffMinutes + ' minutes ago';
        } else if (diffHours < 2) {
            relativeTime = diffHours + ' hour ago';
        } else if (diffHours < 24) {
            relativeTime = diffHours + ' hours ago';
        } else if (diffDays < 2) {
            relativeTime = diffDays + ' day ago';
        } else if (diffDays < 7) {
            relativeTime = diffDays + ' days ago';
        } else {
            relativeTime = diffWeeks + ' weeks ago';
        }
        element.textContent = relativeTime;

    });
}

document.addEventListener('DOMContentLoaded', () => {
    uploadImage();
    initForms();
    handleDropDowns();
    darkModeToggle();
    updateRelativeTimes();
    setInterval(updateRelativeTimes, 6000);
})




   