var Comment = require('./Comment.jsx');

var Comments = React.createClass({

  getInitialState: function() {
    this.resources = AlbatrosBalises.modules[this.props.moduleId].resources;
    this.service = AlbatrosBalises.modules[this.props.moduleId].service;
    return {
      comments: this.props.comments,
      commentCount: this.props.totalComments,
      canLoadMore: (this.props.totalComments > this.props.comments.length) ? true : false,
      lastPage: 0
    }
  },

  render: function() {
    var submitPanel = <div />;
    var commentList = this.state.comments.map(function(item) {
      return <Comment moduleId={this.props.moduleId} comment={item} key={item.CommentId} 
                      appPath={this.props.appPath} onDelete={this.onCommentDelete} />
    }.bind(this));
    if (this.props.loggedIn) {
      submitPanel = (
        <div className="panel-form">
          <div>
           <textarea className="form-control" ref="txtComment" placeholder={this.resources.Comments} />
          </div>
          <div className="panel-form-button">
           <button className="btn btn-primary" ref="cmdAdd" onClick={this.addComment}>{this.resources.AddComment}</button>
          </div>
         </div>
      );
    }
    return (
      <div className="row">
        <div className="col-xs-12">
          <div className="panel panel-default widget">
           <div className="panel-heading">
            <span className="fa fa-comments"></span>
            <h3 className="panel-title">{this.resources.Comments}</h3>
            <span className="label label-info">{this.state.commentCount}</span>
           </div>
           {submitPanel}
           <div className="panel-body">
            <ul className="list-group">
              {commentList}
            </ul>
            <a href="#" className="btn btn-primary btn-sm btn-block" role="button" 
               onClick={this.loadMoreComments} ref="cmdMore" disabled={!this.state.canLoadMore}>
               <span className="fa fa-repeat"></span> {this.resources.More}
            </a>
           </div>
          </div>
        </div>
       </div>
    );
  },

  componentDidMount: function() {
    this.lastCheck = new Date();
  },

  addComment: function(e) {
    e.preventDefault();
    var comment = this.refs.txtComment.getDOMNode().value;
    this.service.addComment(this.props.flightId, comment, function(data) {
      this.refs.txtComment.getDOMNode().value = '';
      var newComments = this.state.comments;
      newComments.unshift(data);
      this.setState({
        comments: newComments,
        commentCount: this.state.commentCount + 1
      });
    }.bind(this));
    return false;
  },

  loadMoreComments: function(e) {
    e.preventDefault();
    if (this.state.canLoadMore) {
      this.service.loadComments(this.props.flightId, this.state.lastPage + 1, this.props.pageSize, function(data) {
        var newCommentList = this.state.comments;
        newCommentList = newCommentList.concat(data);
        this.setState({
          comments: newCommentList,
          lastPage: this.state.lastPage + 1,
          canLoadMore: (data.length < this.props.pageSize) ? false : true
        });
      }.bind(this));
    }
  },

  onCommentDelete: function(commentId, e) {
    e.preventDefault();
    if (confirm(this.resources.CommentDeleteConfirm)) {
      this.service.deleteComment(this.props.flightId, commentId, function() {
        var newCommentList = [];
        for (i = 0; i < this.state.comments.length; i++) {
          if (this.state.comments[i].CommentId != commentId) {
            newCommentList.push(this.state.comments[i]);
          }
        }
        this.setState({
          comments: newCommentList,
          commentCount: this.state.commentCount - 1
        });
      }.bind(this));
    }
  }

});

module.exports = Comments;