import { showImagePreview } from './utility.js';
if ($('#content').length > 0) {
  ClassicEditor.create(document.querySelector('#content')).catch((error) => {
    console.error(error);
  });
}
