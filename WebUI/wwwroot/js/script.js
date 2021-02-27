



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
