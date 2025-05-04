


const connection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .build()


connection.on("ReceiveNotification", function (notification) {
    
    const notifications = document.querySelector('.notifications')

    const content = document.createElement('div')
    content.className = 'notification-content'
    content.setAttribute('data-id', notificationId)

    content.innerHTML =
        
        `
                <img class="notification-image" src="/${notification.icon}">
                <div class="notification-info">
                <div class="message">${notification.message}</div>
                <div class="time" data-created="${new Date(notification.created).toISOString()}">${notification.created}</div>
                </div>
                <button class="notification-close-btn" onclick="dismissNotification('${notification.id}')">X</button> 

        `
    notifications.insertBefore(content, notifications.firstChild)
    updateRelativeTimes()
    updateNotificationCount()
})


connection.on("DismissNotification", function(notificationId) {
    removeNotification(notificationId)
})

connection.start().catch(error => console.error(error))

async function dismissNotification(notificationId) {

    console.log("dismissNotification called with ID:", notificationId);
    try {
        console.log("Fetching to dismiss:", notificationId);
        const res = await fetch(`/api/notifications/dismiss/${notificationId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        })

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



//connection.on("ReceiveNotification", function (notification) {
//    handleNotification(notification, "all");
//});

//connection.on("AdminReceiveNotification", function (notification) {
//    handleNotification(notification, "admin");
//});
