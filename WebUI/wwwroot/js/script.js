function initMap() {

    const bisikletMarket = { lat: -25.344, lng: 131.036 };
    
    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 4,
        center: bisikletMarket,
    });
    
    const marker = new google.maps.Marker({
        position: bisikletMarket,
        map: map,
    });
}

var galleryThumbs = new Swiper('.gallery-thumbs', {
    spaceBetween: 10,
    slidesPerView: 4,
    freeMode: true,
    watchSlidesVisibility: true,
    watchSlidesProgress: true,
});
var galleryTop = new Swiper('.gallery-top', {
    spaceBetween: 10,
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
    },
    thumbs: {
        swiper: galleryThumbs
    }
});