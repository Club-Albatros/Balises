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
    var googleScript = 'https://maps.googleapis.com/maps/api/js';
    if (this.props.apiKey != undefined && this.props.apiKey !== '') {
      googleScript += '?key=' + this.props.apiKey;
    }
    window.loadScript(googleScript, function() {
      var mapDiv = $(this.refs.mapDiv.getDOMNode());
      mapDiv.width(mapDiv.parent().width());
      mapDiv.height(mapDiv.parent().height());
      this._map = new google.maps.Map(this.refs.mapDiv.getDOMNode(), {
        scrollwheel: false,
        mapTypeId: google.maps.MapTypeId.TERRAIN
      });
      this._map.fitBounds(this.getBounds());
      var linePath = [];
      linePath.push({
        lat: this.props.flight.Takeoff.Latitude, 
        lng: this.props.flight.Takeoff.Longitude
      });
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
      var line = new google.maps.Polyline({
        path: linePath,
        geodesic: true,
        strokeColor: "#FF0000",
        strokeOpacity: 0.5,
        strokeWeight: 3
      });
      line.setMap(this._map);
      this.addPointToMap(this.props.flight.Takeoff, 'D');
      this.addPointToMap(this.props.flight.Landing, 'A');
      for (var i=0;i<this.props.track.length;i++)
      {
        this.addPointToMap(this.props.track[i], '');
      }
    }.bind(this));
  },

  addPointToMap: function(point, label) {
    var marker = new google.maps.Marker({
      position: new google.maps.LatLng(point.Latitude, point.Longitude),
      map: this._map,
      label: label,
      title: point.Name + ' ' + point.PassageTime.substr(11,5)
    });
  },

  getBounds: function() {
    var minLat = this.props.flight.Takeoff.Latitude, 
        maxLat = this.props.flight.Takeoff.Latitude, 
        minLong = this.props.flight.Takeoff.Longitude, 
        maxLong = this.props.flight.Takeoff.Longitude;
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