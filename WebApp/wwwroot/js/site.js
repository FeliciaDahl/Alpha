/*Notification*/

//I VIDEO 45MIN , KOLLA CONTAINER SÅ DET BLIR RÄTT DESSUTOM NOT-CONTENT.

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .build()

connection.on("ReceiveMessage", function (notification) {
    const container = document.querySelector('.notifications')
    const content = document.createElement('div')
    content.className = 'notification-content'
    content.setAttribute('data-id', notification.id)
    content.innerHTML =

        `
                <img class="norification-image"src="${notification.icon}">
                <div class="notification-info">
                <div class="message">${notification.message}</div>
                <div class="time data-created="${new Date(notification.created).toISOString()}">${notification.created}</div>
                </div>
                <button class="notification-close-btn" onclick="dismissNotification('${notification.id}')">X</button>
        `
    container.insertBefore(content, container.firstChild)
    updateRelativeTimes()
    updateNotificationCount()
})

connection.on("NotificationDismissed", function(notificationId)) {
    removeNotification(notificationId)
}

connection.start().catch(error => console.error(error))

async function dismissNotification(notificationId) {
    try {

        const res = await fetch(`/api/notifications/dismiss/${notificationId}`, { method: 'POST' })
        if (res.ok) {
            removeNotification(notificationId)
        }
        else {
            console.error('Failed to remove notification:')
        }
    }
    catch (error) {
        console.error('Failed to remove notification:', error)
    }
}

function removeNotification(notificationId) {
    const element = document.querySelector(`.notification-content[data-id="${notificationId}"]`)
    if (element) {
        element.remove()
        updateNotificationCount()
    }
}

function updateNotificationCount() {
    const notifications = document.querySelector('.notifications')
    const notificationNumber = document.querySelector('.notification-number')
    const notificationDropDownBtn = document.querySelector('.btn-notification')
    const count = notifications.querySelectorAll('.notification-content').length

    if (notificationNumber) {
        notificationNumber.textContent = count
    }

    let dot = notificationDropDownBtn.querySelector('.dot.dot-red')
    if (count > 0 && !dot) {
        dot = document.createElement('div')
        dot.className = 'dot dot-red'
        notificationDropDownBtn.appendChild(dot)
    }
    if (count === 0 && dot) {
        dot.remove()
    }
}

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

document.addEventListener('DOMContentLoaded', () => {
    uploadImage();
    initForms();
    handleDropDowns()
    darkModeToggle();
})


   