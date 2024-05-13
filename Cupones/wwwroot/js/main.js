document.addEventListener('scroll', () => {
    const nav = document.querySelector('div')
    const ull = document.querySelector('ul')
    if (window.scrollY > 0) {
        nav.classList.add('scrolled')
    } else {
        nav.classList.remove('scrolled')
    }
})

const some = document.getElementById('react-btn')

some.onclick = function (event) {
    console.log(event.target)
}