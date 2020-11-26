export function loadLocationMap() {
    var _loaded = {};
    var url = "https://cdn.jsdelivr.net/gh/openlayers/openlayers.github.io@master/en/v6.4.3/build/ol.js";

    var s = document.createElement('script');
    s.src = url;
    document.head.appendChild(s);
    _loaded[url] = true;

    s.addEventListener('load', () => {
        initializeMap()
    })

    function initializeMap() {
        var map = new ol.Map({
            target: 'map',
            layers: [
                new ol.layer.Tile({
                    source: new ol.source.OSM()
                })
            ],
            view: new ol.View({
                center: ol.proj.fromLonLat([-86.20, 43.07]),
                zoom: 10
            })
        });
    }
}
