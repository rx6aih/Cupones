document.addEventListener('scroll', () => {
    const nav = document.querySelector('div')
    const ull = document.querySelector('ul')
    if (window.scrollY > 0) {
        ull.classList.add('bh-c')
        nav.classList.add('scrolled')
    } else {
        nav.classList.remove('scrolled')
        ull.classList.remove('bh-c')
    }
})