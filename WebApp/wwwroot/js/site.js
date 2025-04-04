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

    /*Handle Form Submission*/

    
})