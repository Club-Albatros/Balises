var BeaconRow = require('./BeaconRow.jsx');

module.exports = React.createClass({

  newId: 0,

  getInitialState: function() {
    this.resources = AlbatrosBalises.modules[this.props.moduleId].resources;
    this.service = AlbatrosBalises.modules[this.props.moduleId].service;
    var initialBeaconList = this.props.track.map(function(b) {
      this.newId++;
      return {
        Id: this.newId,
        BeaconId: b.BeaconId,
        Name: b.Name,
        PassOrder: b.PassOrder
      }
    }.bind(this));
    return {
      passedBeacons: initialBeaconList
    }
  },

  render: function() {
    console.log(this.state.passedBeacons);
    var beaconList = this.state.passedBeacons.map(function(elem) {
      console.log(elem.Id);
      return <BeaconRow beacon={elem} moduleId={this.props.moduleId}
                        deleteBeacon={this.deleteBeacon}
                        key={elem.Id} />;
    }.bind(this));
    var iconCol = {
      width: '40px'
    };
    var timeCol = {
      width: '120px'
    };
    return (
      <div className="row" ref="row">
       <div className="col-xs-12 col-md-8" ref="leftPane">
        <div ref="mapDiv"></div>
       </div>
       <div className="col-xs-12 col-md-4" ref="rightPane">
        <table className="table">
         <thead>
           <tr>
            <th>{this.resources.Beacon}</th>
            <th style={iconCol}></th>
           </tr>
         </thead>
         <tbody ref="beaconList">
          {beaconList}
         </tbody>
        </table>
        <input type="hidden" ref="BeaconList" id="BeaconList" name="BeaconList" value="" />
       </div>
      </div>
    );
  },

  componentDidMount: function() {
    this.refs.BeaconList.getDOMNode().value = JSON.stringify(this.state.passedBeacons);
    var googleScript = this.props.scheme + '://maps.googleapis.com/maps/api/js';
    if (this.props.apiKey != undefined && this.props.apiKey !== '') {
      googleScript += '?key=' + this.props.apiKey;
    }
    window.loadScript(googleScript, function() {
      this.expandDivToParent($(this.refs.row.getDOMNode()));
      this.expandDivToParent($(this.refs.leftPane.getDOMNode()));
      this.expandDivToParent($(this.refs.mapDiv.getDOMNode()));
      this._map = new google.maps.Map(this.refs.mapDiv.getDOMNode(), {
        scrollwheel: false,
        mapTypeId: google.maps.MapTypeId.TERRAIN
      });
      this._map.fitBounds(this.getBounds());
      for (var i=0;i<this.props.beacons.length;i++)
      {
        this.addPointToMap(this.props.beacons[i]);
      }
    }.bind(this));
    $(this.refs.beaconList.getDOMNode()).sortable({
      update: function (event, ui) {
        this.resortBeacons();
      }.bind(this)
    });
  },

  componentDidUpdate: function() {
    this.refs.BeaconList.getDOMNode().value = JSON.stringify(this.state.passedBeacons);
  },

  expandDivToParent: function(div) {
    div.height(div.parent().height());
  },

  addPointToMap: function(point) {
    var marker = new google.maps.Marker({
      position: new google.maps.LatLng(point.Latitude, point.Longitude),
      map: this._map,
      label: point.Code,
      title: point.Code + ' ' + point.Name + ' (' + point.Points + ' pts)',
      beaconId: point.BeaconId,
      name: point.Name
    });
    marker.addListener('click', function(e) {
      this.addBeaconToList(marker);
    }.bind(this));
  },

  addBeaconToList: function(b) {
    this.newId++;
    var beacon = {
      Id: this.newId,
      BeaconId: b.beaconId,
      Name: b.name,
      PassOrder: this.state.passedBeacons.length + 1
    };
    var newBeaconList = this.state.passedBeacons;
    newBeaconList.push(beacon);
    this.setState({
      passedBeacons: newBeaconList
    });
  },

  getBounds: function() {
    var minLat = this.props.beacons[0].Latitude, 
        maxLat = this.props.beacons[0].Latitude, 
        minLong = this.props.beacons[0].Longitude, 
        maxLong = this.props.beacons[0].Longitude;
    for (var i=1;i<this.props.beacons.length;i++)
    {
      if(this.props.beacons[i].Latitude < minLat) {
        minLat = this.props.beacons[i].Latitude;
      }
      if(this.props.beacons[i].Latitude > maxLat) {
        maxLat = this.props.beacons[i].Latitude;
      }
      if(this.props.beacons[i].Longitude < minLong) {
        minLong = this.props.beacons[i].Longitude;
      }
      if(this.props.beacons[i].Longitude > maxLong) {
        maxLong = this.props.beacons[i].Longitude;
      }
    }
    return new google.maps.LatLngBounds({
      lat: minLat,
      lng: minLong
    }, {
      lat: maxLat,
      lng: maxLong
    });
  },

  deleteBeacon: function(beacon, e) {
    e.preventDefault();
    var newBeaconList = [];
    var newOrder = 1;
    for (i=0;i<this.state.passedBeacons.length;i++) {
      var oldBeacon = this.state.passedBeacons[i];
      if (oldBeacon.Id != beacon.Id) {
        oldBeacon.PassOrder = newOrder;
        newOrder++;
        newBeaconList.push(oldBeacon);
      }
    }
    this.setState({
      passedBeacons: newBeaconList
    });
  },

  resortBeacons: function() {
    var newList = [];
    $(this.refs.beaconList.getDOMNode()).children().each(function(i, el) {
      newList.push({
        Id: parseInt(el.getAttribute("data-id")),
        BeaconId: el.getAttribute("data-beaconid"),
        Name: el.getAttribute("data-name"),
        PassOrder: i + 1
      })
    });
    console.log(newList);
    return null;
    this.setState({
      passedBeacons: newList      
    });
  }


});
