$(function () {
  var INDEX = 0;
  const firstMessage = `<div id="cm-msg-0" class="chat-msg user" style="display:none">
														<span class="msg-avatar"><img src="/images/girl-card.jpeg"></span>
														<div class="cm-msg-text">Xin chào tôi là Angela Phương Trinh, tôi có thể giúp gì được cho bạn 🙂
														</div>
													</div>`;
  $('#chat-submit').click(function (e) {
    e.preventDefault();
    var msg = $('#chat-input').val();
    if (msg.trim() == '') {
      return false;
    }
    generate_message(msg, 'self');
    var buttons = [
      {
        name: 'Existing User',
        value: 'existing',
      },
      {
        name: 'New User',
        value: 'new',
      },
    ];
    setTimeout(function () {
      generate_message(msg, 'user');
    }, 1000);
  });

  function generate_message(msg, type) {
    INDEX++;
    var str = '';
    str += "<div id='cm-msg-" + INDEX + '\' class="chat-msg ' + type + '">';
    str += '          <span class="msg-avatar">';
    if (type == 'user') {
      str += '            <img src="/images/girl-card.jpeg">';
    } else {
      str += '            <img src="/images/avatar-default.jpg">';
    }
    str += '          </span>';
    str += '          <div class="cm-msg-text">';
    if (type == 'user') {
      $.ajax({
        method: 'GET',
        url: 'https://api.chucknorris.io/jokes/random',
        success: function (result) {
          str += result.value;
          str += '          </div>';
          str += '        </div>';
          $('.chat-logs').append(str);
          $('#cm-msg-' + INDEX)
            .hide()
            .fadeIn(300);
          if (type == 'self') {
            $('#chat-input').val('');
          }
          $('.chat-logs')
            .stop()
            .animate({ scrollTop: $('.chat-logs')[0].scrollHeight }, 1000);
        },
        error: function ajaxError(jqXHR) {
          console.error('Error: ', jqXHR.responseText);
        },
      });
    } else {
      str += msg;
      str += '          </div>';
      str += '        </div>';
      $('.chat-logs').append(str);
      $('#cm-msg-' + INDEX)
        .hide()
        .fadeIn(300);
      if (type == 'self') {
        $('#chat-input').val('');
      }
      $('.chat-logs')
        .stop()
        .animate({ scrollTop: $('.chat-logs')[0].scrollHeight }, 1000);
    }
  }

  $(document).delegate('.chat-btn', 'click', function () {
    var value = $(this).attr('chat-value');
    var name = $(this).html();
    $('#chat-input').attr('disabled', false);
    generate_message(name, 'self');
  });

  $('#chat-circle').click(function () {
    $('#chat-circle').toggle('scale');
    $('.chat-box').toggle('scale');
    $('.chat-logs').append(firstMessage);
    setTimeout(() => {
      $('#cm-msg-0').hide().fadeIn(300);
    }, 1000);
  });

  $('.chat-box-toggle').click(function () {
    console.log('Toggle');
    $('#chat-circle').toggle('scale');
    $('.chat-box').toggle('scale');
  });
});
