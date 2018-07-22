const applicationServerPublicKey = 'BMxSsstoKoPd2UiGJNL7-NJYt9uwWV9e8lvVOCbvPcVvlj0ogAzkq7YXibGp-S8u6EwXUKR81b7TezRjvK3lyfo';
const pushButton = document.querySelector('.js-push-btn');

let isSubscribed = false;
let swRegistration = null;

if ('serviceWorker' in navigator && 'PushManager' in window) {
    console.log('Service Worker and Push is supported');

    navigator.serviceWorker.register('js/sw.js')
        .then(function (swReg) {
            console.log('Service Worker is registered', swReg);

            swRegistration = swReg;
            initializeUI();
        })
        .catch(function (error) {
            console.error('Service Worker Error', error);
        });
} else {
    console.warn('Push messaging is not supported');
    pushButton.textContent = 'Push Not Supported';
}

function urlB64ToUint8Array(base64String) {
    const padding = '='.repeat((4 - base64String.length % 4) % 4);
    const base64 = (base64String + padding)
        .replace(/\-/g, '+')
        .replace(/_/g, '/');

    const rawData = window.atob(base64);
    const outputArray = new Uint8Array(rawData.length);

    for (let i = 0; i < rawData.length; ++i) {
        outputArray[i] = rawData.charCodeAt(i);
    }
    return outputArray;
}

function initializeUI() {
    pushButton.addEventListener('click', function () {
        pushButton.disabled = true;
        if (isSubscribed) {
            unSubscribe();
        } else {
            subscribeUser();
        }
    });


    // Set the initial subscription value
    swRegistration.pushManager.getSubscription()
        .then(function (subscription) {
            isSubscribed = !(subscription === null);

            if (isSubscribed) {
                console.log('User IS subscribed.');
            } else {
                console.log('User is NOT subscribed.');
            }

            updateBtn();
        });
}

function updateBtn() {
    if (Notification.permission === 'denied') {
        pushButton.textContent = 'Push Messaging Blocked.';
        pushButton.disabled = true;
        unSubscribe();
        return;
    }

    if (isSubscribed) {
        pushButton.textContent = 'Disable Push Messaging';
    } else {
        pushButton.textContent = 'Enable Push Messaging';
    }

    pushButton.disabled = false;
}

function subscribeUser() {
    const applicationServerKey = urlB64ToUint8Array(applicationServerPublicKey);
    swRegistration.pushManager.subscribe({
        userVisibleOnly: true,
        applicationServerKey: applicationServerKey
    })
    .then(function (subscription) {
        console.log('User is subscribed.');

        fetch('api/subscription', {
            headers: { 'Content-Type': 'application/json' },
            method: 'POST',
            credentials: 'same-origin',
            body: JSON.stringify(subscription)
        })
        .then(res => {
            isSubscribed = true;

            updateBtn();
        })
        .catch(function (err) {
            console.log('Failed to send data to server: ', err);
        })
    })
    .catch(function (err) {
        console.log('Failed to subscribe the user: ', err);
        updateBtn();
    });
}

function unSubscribe() {
    swRegistration.pushManager.getSubscription()
        .then(function (subscription) {
            if (subscription) {
                subscription.unsubscribe()
                    .catch(function (error) {
                        console.log('Error unsubscribing', error);
                    })
                    .then(function () {
                        console.log('User is unsubscribed from browser.');

                        fetch('api/subscription?endPoint=' + subscription.endpoint, {
                            headers: { 'Content-Type': 'application/json' },
                            method: 'DELETE',
                            credentials: 'same-origin'
                        })
                        .then(res => {
                            console.log('User is unsubscribed from server.');

                            isSubscribed = false;

                            updateBtn();
                        })
                    });
            }
        })
}