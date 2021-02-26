var myFullpage = new fullpage('#fullpage', {
    autoscrolling: false,
    navigation: true,
    controlArrows: false,
    anchors: ['homepage', 'about', 'sales', 'contact'],
    sectionsColor: ['black', '#e9c46a', '#f4a261', '#e76f51']
});

setInterval(function () {
    fullpage_api.moveSlideRight();;
}, 3000);


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
