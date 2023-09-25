using Homestay_Management.Data;
using Homestay_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Homestay_Management.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        private readonly DataContext _dataContext;//Sử dụng private readonly để đảm bảo tính ổn định trong suốt quá trình chạy ứng dụng
        public HomeAdminController(DataContext dataContext)  //Constructor
        {
            _dataContext = dataContext;           //Khởi tạo _dataContext từ tham số truyền vào
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }


        [Route("listroom")]    //Khai báo đường dẫn
        public IActionResult ListRoom(int? page)   //Để phân trang được ta cần add trong Nuget:                                                 x.PagedList.Mvc.Core và x.PagedList
        {
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listRoom = _dataContext.tblRoom
                            .Include(r => r.TypeRoom) // Kết hợp thông tin Tên loại Phòng
                            .AsNoTracking().OrderBy(r => r.Name)
                            .ToList();
            PagedList<RoomModel> list = new PagedList<RoomModel>(listRoom, pageNumber, pageSize); 
            return View(list);
        }


        //Add Room
        [Route("addroom")]             //Đường dẫn Url mà phương thức này sẽ xử lý
        [HttpGet]                     //Khi truy cập URL "/addroom" bằng cách gõ vào trình duyệt                              or nhấp vào 1 liên kết, phương thức AddRoom sẽ được gọi.
        public IActionResult AddRoom()
        {
            //Lấy TypeRoomId hiển thị lên Name của TypeRoomId
            ViewBag.TypeRoomId = new SelectList(_dataContext.tblTypeRoom.ToList(),"Id","Name");
            return View();
        }
        [Route("addroom")]
        [HttpPost]
        [ValidateAntiForgeryToken]//Đảm bảo rằng yêu cầu POST phải đi kèm với một mã thông báo được sinh ra động, và phương thức này kiểm tra tính hợp lệ của mã thông báo trước khi xử lý dữ liệu.Nó bảo vệ bạn khỏi cuộc tấn công of các tin tặc Cross-Site Request Forgery (CSRF).
        public IActionResult AddRoom(RoomModel room)
        {
            ModelState.Remove("TypeRoom");            //Loại bỏ lỗi kiểm tra cho thuộc tính
            ModelState.Remove("ImageUpload");
            if(ModelState.IsValid)                    //Nếu dữ liệu hợp lệ
            {
                _dataContext.tblRoom.Add(room);       //Thêm đối room vào CSDL
                _dataContext.SaveChanges();           //Lưu vào CSDL
                return RedirectToAction("listroom");  //Trả về trang danh sách các phòng
            }
            return View(room);                        //Nhập lại khi nhập sai các yêu cầu trên
        }


        //Edit Room
        [Route("editroom")]             //Đường dẫn Url mà phương thức này sẽ xử lý
        [HttpGet]                     //Khi truy cập URL "/addroom" bằng cách gõ vào trình duyệt                              or nhấp vào 1 liên kết, phương thức AddRoom sẽ được gọi.
        public IActionResult EditRoom(int roomId)
        {
            //Lấy TypeRoomId hiển thị lên Name của TypeRoomId
            ViewBag.TypeRoomId = new SelectList(_dataContext.tblTypeRoom.ToList(), "Id", "Name");
            var room = _dataContext.tblRoom.Find(roomId);
            return View(room);
        }
        [Route("editroom")]
        [HttpPost]
        [ValidateAntiForgeryToken]//Đảm bảo rằng yêu cầu POST phải đi kèm với một mã thông báo được sinh ra động, và phương thức này kiểm tra tính hợp lệ của mã thông báo trước khi xử lý dữ liệu.Nó bảo vệ bạn khỏi cuộc tấn công of các tin tặc Cross-Site Request Forgery (CSRF).
        public IActionResult EditRoom(RoomModel room)
        {
            ModelState.Remove("TypeRoom");            //Loại bỏ lỗi kiểm tra cho thuộc tính
            ModelState.Remove("ImageUpload");
            if (ModelState.IsValid)                    //Nếu dữ liệu hợp lệ
            {
                _dataContext.Update(room);       
                //Cách 2: _dataContext.Entry(room).State = EntityState.Modified;
                _dataContext.SaveChanges();           //Lưu vào CSDL
                return RedirectToAction("listroom");  //Trả về trang danh sách các phòng
            }
            return View(room);                        //Nhập lại khi nhập sai các yêu cầu trên
        }


        //DeleteRoom
        [Route("deleteroom")]
        [HttpGet]
        public IActionResult DeleteRoom(int roomId) 
        {
            //Trường hợp này khi nếu có hóa đơn đặt phòng rồi thì không được xóa.Mấy nữa làm đến bảng hóa đơn thì cần cho vào
            //TempData["Message"] = "This room cannot be deleted because it has already been booked.";
            //var bill = _dataContext.tblBill.Where(x => x.Id == roomId);
            //if(bill.Cout() > 0)
            //{
            //    return RedirectToAction("listroom");
            //}    


            //Mấy nữa thiết kế nếu hình ảnh để ra 1 bảng riêng thì sẽ xóa cả hình ảnh liên quan ở đây
            //var imageRoom = _dataContext.tblImage.Where(x => x.Id == roomId);
            //if(imageRoom.Any()) //Nếu có ít nhất 1 ảnh
            //{
            //    _dataContext.RemoveRange(imageRoom);  //Xóa hàng loạt
            //}    


            _dataContext.Remove(_dataContext.tblRoom.Find(roomId));
            _dataContext.SaveChanges();
            TempData["Message"] = "Room has been deleted";
            return RedirectToAction("listroom");
        }
    }
}
