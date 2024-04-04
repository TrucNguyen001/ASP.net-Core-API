import tinyEmitter from "tiny-emitter/instance";
import helper from "../helper/helper";
// Định nghĩa một biến để theo dõi trạng thái Ctrl
let ctrlPressed = false;

// Xử lý sự kiện khi người dùng nhấn phím
document.addEventListener(helper.MKeyBoard.KeyDown, function (event) {
  // Kiểm tra xem phím Ctrl có được nhấn không
  if (event.key === helper.MKeyBoard.Control) {
    ctrlPressed = true;
  }

  // Kiểm tra nếu người dùng nhấn Ctrl + D
  if (event.key === helper.MKeyBoard.D && ctrlPressed) {
    event.preventDefault();
    tinyEmitter.emit(helper.Emitter.SendEvent, helper.Status.Delete);
  }

  // Kiểm tra nếu người dùng nhấn Ctrl + S
  if (event.key === helper.MKeyBoard.S && ctrlPressed) {
    event.preventDefault();
    tinyEmitter.emit(helper.Emitter.SendEvent, helper.Status.Save);
  }

  // Kiểm tra nếu người dùng nhấn Ctrl + E
  if (event.key === helper.MKeyBoard.E && ctrlPressed) {
    event.preventDefault();
    tinyEmitter.emit(helper.Emitter.SendEvent, helper.Status.Update);
  }

  // Kiểm tra nếu người dùng nhấn Ctrl + 1
  if (event.key === helper.MKeyBoard.One && ctrlPressed) {
    event.preventDefault();
    tinyEmitter.emit(helper.Emitter.SendEvent, helper.Status.Insert);
  }

  // Kiểm tra nếu người dùng nhấn Ctrl + 2- Import
  if (event.key === helper.MKeyBoard.Two && ctrlPressed) {
    event.preventDefault();
    tinyEmitter.emit(helper.Emitter.SendEvent, helper.Status.Import);
  }

  // Kiểm tra nếu người dùng nhấn Ctrl + Shift + E- Export
  if (event.shiftKey && event.key === helper.MKeyBoard.Export && ctrlPressed) {
    event.preventDefault();
    tinyEmitter.emit(helper.Emitter.SendEvent, helper.Status.Export);
  }

  // Kiểm tra nếu người dùng nhấn ESC
  if (event.key === helper.MKeyBoard.Escape) {
    event.preventDefault();
    tinyEmitter.emit(helper.Emitter.SendEvent, helper.Status.Exit);
  }

  // Kiểm tra nếu người dùng nhấn CTRL + SHIFT + S
  if (event.shiftKey && event.key === helper.MKeyBoard.SUpper && ctrlPressed) {
    event.preventDefault();
    tinyEmitter.emit(helper.Emitter.SendEvent, helper.Status.SaveAndInsert);
  }

  // Thêm xử lý khi người dùng nhấn nút delete
  if (event.key === "Delete") {
    event.preventDefault(); // Ngăn chặn hành động mặc định của nút delete
    tinyEmitter.emit(helper.Emitter.SendEvent, helper.Status.DeleteMultiple);
  }
});

// Xử lý sự kiện khi người dùng nhả phím
document.addEventListener(helper.MKeyBoard.KeyUp, function (event) {
  // Kiểm tra xem phím Ctrl có được nhả ra không
  if (event.key === helper.MKeyBoard.Control) {
    ctrlPressed = false;
  }
});
