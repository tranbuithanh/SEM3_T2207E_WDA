import { alertSimple, cartDisplay, showImagePreview } from './utility.js';
showImagePreview('#ImageUploadCard', '#previewImageCard');
showImagePreview('#ImageUploadCardText', '#previewImageCardText');
showImagePreview('#ImageUploadLogo', '#previewImageLogo');

$('#Name').keyup(function (e) {
  $('.name-game-preview').text($(this).val());
});
