/** @jsx React.DOM */
var ViewMap = React.createClass({

  getInitialState: function() {
    this.resources = AlbatrosBalises.modules[this.props.moduleId].resources;
    this.service = AlbatrosBalises.modules[this.props.moduleId].service;
    return {
    }
  },

  render: function() {
    return (
      <div ref="mapDiv"></div>
    );
  },

  componentDidMount: function() {
    var googleScript = 'http://maps.googleapis.com/maps/api/js';
    if (this.props.apiKey && this.props.apiKey !== '') {
      googleScript += '&key=' + apiKey;
    }
    window.loadScript(googleScript, function() {
      console.log(this.props.flight);
      var mapDiv = $(this.refs.mapDiv.getDOMNode());
      mapDiv.width(mapDiv.parent().width());
      mapDiv.height(mapDiv.parent().height());
      this._map = new google.maps.Map(this.refs.mapDiv.getDOMNode(), {
        mapTypeId: google.maps.MapTypeId.TERRAIN
      });
      this._map.fitBounds(this.getBounds());
      var linePath = [];
      linePath.push({
        lat: this.props.flight.Start.Latitude, 
        lng: this.props.flight.Start.Longitude
      });
        console.log(this.props.track);
      for (var i=0;i<this.props.track.length;i++)
      {
        linePath.push({
          lat: this.props.track[i].Latitude, 
          lng: this.props.track[i].Longitude
        });
      }
      linePath.push({
        lat: this.props.flight.Landing.Latitude, 
        lng: this.props.flight.Landing.Longitude
      });
      console.log(linePath);
      var line = new google.maps.Polyline({
        path: linePath,
        geodesic: true,
        strokeColor: "#FF0000",
        strokeOpacity: 0.5,
        strokeWeight: 3
      });
      line.setMap(this._map);
    }.bind(this));
  },

  getBounds: function() {
    var minLat = this.props.flight.Start.Latitude, 
        maxLat = this.props.flight.Start.Latitude, 
        minLong = this.props.flight.Start.Longitude, 
        maxLong = this.props.flight.Start.Longitude;
    if(this.props.flight.Landing.Latitude < minLat) {
      minLat = this.props.flight.Landing.Latitude;
    }
    if(this.props.flight.Landing.Latitude > maxLat) {
      maxLat = this.props.flight.Landing.Latitude;
    }
    if(this.props.flight.Landing.Longitude < minLong) {
      minLong = this.props.flight.Landing.Longitude;
    }
    if(this.props.flight.Landing.Longitude > maxLong) {
      maxLong = this.props.flight.Landing.Longitude;
    }
    for (var i=0;i<this.props.track.length;i++)
    {
      if(this.props.track[i].Latitude < minLat) {
        minLat = this.props.track[i].Latitude;
      }
      if(this.props.track[i].Latitude > maxLat) {
        maxLat = this.props.track[i].Latitude;
      }
      if(this.props.track[i].Longitude < minLong) {
        minLong = this.props.track[i].Longitude;
      }
      if(this.props.track[i].Longitude > maxLong) {
        maxLong = this.props.track[i].Longitude;
      }
    }
    return new google.maps.LatLngBounds({
      lat: minLat,
      lng: minLong
    }, {
      lat: maxLat,
      lng: maxLong
    });
  }

});

module.exports = ViewMap;