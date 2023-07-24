using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TruyenVN;

namespace TruyenVNAPI.Model
{
    public class TruyenVNDbContext : DbContext
    {
        public TruyenVNDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                               .AddJsonFile("appsettings.json", true, true)
                                               .Build();
            string? con = connectionString.GetConnectionString("TruyenVNDb");
            optionsBuilder.UseSqlServer(con);
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Chapter> Chapters { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Story> Stories { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Viewed> Vieweds { get; set; }
        public virtual DbSet<StoryCategory> StoryCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StoryCategory>()
               .HasKey(ba => new { ba.cate_id, ba.story_id });

            modelBuilder.Entity<Author>().HasData(
                new Author { author_id = 1, author_name = "Mashima Hiro", author_description = "Mashima Hiro là một họa sĩ manga người Nhật nổi tiếng với ba bộ truyện Rave Master, Fairy Tail và Edens Zero." },
                new Author { author_id = 2, author_name = "Kishimoto Masashi", author_description = "Kishimoto Masashi là một họa sĩ truyện tranh đã được biết đến qua bộ truyện tranh nổi tiếng thế giới Naruto" },
                new Author { author_id = 3, author_name = "Oda Eiichiro", author_description = "Oda Eiichirō là một họa sĩ truyện tranh người Nhật Bản, hiện đang sáng tác cho nhà xuất bản Shūeisha" },
                new Author { author_id = 4, author_name = "Atsuo Ueda", author_description = "Atsuo Ueda là trợ lí cho họa sĩ Mashima Hiro" },
                new Author { author_id = 5, author_name = "Fujiko Fujio", author_description = "Fujiko Fujio là bút danh chung của hai nghệ sĩ manga Nhật Bản, người Việt gọi là Ông Hai Phú hay Ông Phú Sĩ." },
                new Author { author_id = 6, author_name = "Đường Gia Tam Thiếu", author_description = "Trương Uy, là một tiểu thuyết gia, văn học mạng người Trung Quốc, được biết đến với bút danh Đường Gia Tam Thiếu." },
                new Author { author_id = 7, author_name = "Umemura Shinya", author_description = "Chưa có thông tin" },
                new Author { author_id = 8, author_name = "Aoyama Gōshō", author_description = "Ông là một mangaka nổi tiếng với các tác phẩm như Yaiba, Magic Kaito hay Thám tử lừng danh Conan." },
                new Author { author_id = 9, author_name = "Thiên Tằm Thổ Đậu", author_description = "Anh bắt đầu viết tiểu thuyết khi còn mới là học sinh trung học và dần trở thành một trong những cây bút tiêu biểu của văn học mạng Trung Quốc" },
                new Author { author_id = 10, author_name = "Đang cập nhật", author_description = "Đang cập nhật" }
                );

            modelBuilder.Entity<Story>().HasData(
                new Story { story_id = 1, story_name = "Hội pháp sư - Fairy Tail", author_id = 1, description = "Xuyên suốt câu chuyện là cuộc phiêu lưu của một Sorceress tên là Lucy Heartphilia, sau khi cô tham gia vào hiệp hội hội Fairy Tail, cô đã cùng với Natsu Dragneel hành trình để đi tìm một con rồng tên là Igneel.", View = 0, story_image = "https://news-w.com/wp-content/uploads/2022/03/ve-hoi-phap-su-thumbnail.jpg", create_at = DateTime.Today, update_at = DateTime.Today, isComic = true },
                new Story { story_id = 2, story_name = "Naruto", author_id = 2, description = "Câu chuyện xoay quanh Uzumaki Naruto, một ninja trẻ muốn tìm cách khẳng định mình để được mọi người công nhận và nuôi ước mơ trở thành Hokage - người lãnh đạo Làng Lá. ", View = 0, story_image = "https://upload.wikimedia.org/wikipedia/en/9/94/NarutoCoverTankobon1.jpg", create_at = DateTime.Today, update_at = DateTime.Today, isComic = true },
                new Story { story_id = 3, story_name = "Đảo Hải Tặc - One Piece", author_id = 3, description = ". One Piece kể về cuộc hành trình của Monkey D. Luffy - thuyền trưởng của băng hải tặc Mũ Rơm và các đồng đội của cậu. Luffy tìm kiếm vùng biển bí ẩn nơi cất giữ kho báu lớn nhất thế giới One Piece, với mục tiêu trở thành Vua Hải Tặc.", View = 0, story_image = "https://i.truyenvua.com/slider/290x191/slider_1559211185.jpg?gt=hdfgdfg&mobile=2", create_at = DateTime.Today, update_at = DateTime.Today, isComic = true },
                new Story { story_id = 4, story_name = "Fairy Tail - Nhiệm vụ trăm năm", author_id = 4, description = "Phần tiếp theo của loạt truyện đình đám cùng tên khi nhóm Natsu đi làm nhiệm vụ trăm năm", View = 0, story_image = "https://i.truyenvua.com/slider/290x191/slider_1561609693.jpg?gt=hdfgdfg&mobile=2", create_at = DateTime.Today, update_at = DateTime.Today, isComic = true },
                new Story { story_id = 5, story_name = "Doraemon", author_id = 5, description = "Trong truyện lấy bối cảnh ở thế kỷ 22, Doraemon là chú mèo robot của tương lai do xưởng Matsushiba — công xưởng chuyên sản xuất robot vốn dĩ nhằm mục đích chăm sóc trẻ nhỏ.", View = 0, story_image = "https://vcdn.tikicdn.com/cache/450x450//media/catalog/product/i/m/img225_11.jpg", create_at = DateTime.Today, update_at = DateTime.Today, isComic = true },
                new Story { story_id = 6, story_name = "Đấu la đại lục", author_id = 6, description = "câu chuyện về Đường Tam, một thiên tài chế tạo ám khí của đường môn đã xuyên không đến Đấu La Đại Lục.", View = 0, story_image = "https://hhhkungfu.tv/wp-content/uploads/Dau-La-Dai-Luc-2.jpg", create_at = DateTime.Today, update_at = DateTime.Today, isComic = false },
                new Story { story_id = 7, story_name = "Conan", author_id = 7, description = "Truyện xoay quanh một cậu thám tử trung học có tên là Kudo Shinichi trong lúc đang điều tra một Tổ chức Áo đen bí ẩn đã bị ép phải uống một loại thuốc độc có thể gây chết người. May mắn là cậu đã sống sót nhưng cơ thể thì lại bị teo nhỏ như một đứa bé 6 tuổi. Kể từ đó để tránh bị lộ thân phận thực sự của mình, cậu đã lấy tên là Edogawa Conan và chuyển đến sống ở nhà của cô bạn thời thơ ấu Mori Ran cùng với bố của cô ấy là một thám tử tư tên Mori Kogoro với hy vọng một ngày nào đó cậu có thể hạ gục Tổ chức Áo Đen và lấy lại hình dáng ban đầu.", View = 0, story_image = "https://im.uniqlo.com/global-cms/spa/rese7e57a44a646f56f9cf35532a9777337fr.jpg", create_at = DateTime.Today, update_at = DateTime.Today, isComic = true },
                new Story { story_id = 8, story_name = "Đấu phá thương khung", author_id = 8, description = "Đấu Phá Thương Khung là một câu chuyện huyền huyễn đặc sắc kể về Tiêu Viêm, một thiên chi kiêu tử với thiên phú tu luyện mà ai ai cũng hâm mộ, bỗng một ngày người mẹ mất đi đễ lại di vật là một chiếc giới chỉ màu đen nhưng từ khi đó Tiêu Viêm đã mất đi thiên phú tu luyện của mình.\r\n\r\n- Từ thiên tài rớt xuống làm phế vật trong 3 năm, rồi bị vị hôn thê thẳng thừng từ hôn, làm dấy lên ý chí nam nhi của mình, Tiêu Viêm nhờ di vật của mẫu thân để lại là 1 chiếc hắc giới chỉ (nhẫn màu đen)Tiêu Viêm gặp được hồn của Dược Lão (Dược Trần – Dược tôn giả) 1 đại luyện dược tông sư của đấu khí đại lục…", View = 0, story_image = "https://img.meta.com.vn/Data/image/2021/11/01/lich-chieu-dau-pha-khung-thuong-ova-3.jpg", create_at = DateTime.Today, update_at = DateTime.Today, isComic = false },
                new Story { story_id = 9, story_name = "Thật Đáng Thương Cho Chúng Tôi", author_id = 10, description = "“Hãy chia tay với tên khốn đó đi”\r\nCái gì? Cậu là bạn của tôi đó…!\r\n“Sang Jae Soo” người muốn tôi và người yêu chia tay. Và tôi, người không thể đẩy cậu ấy ra xa, ‘Ha Won Soo.", View = 0, story_image = "https://i.truyenvua.com/ebook/190x247/that-dang-thuong-cho-chung-toi_1688092650.jpg?gt=hdfgdfg&mobile=2", create_at = DateTime.Today, update_at = DateTime.Today, isComic = true },
                new Story { story_id = 10, story_name = "Mở Đầu Ta Ngược Đãi Nam Chủ", author_id = 10, description = "Nhan Xu một thời xuyên qua trên người nữ phụ độc ác của tiểu thuyết nam tần, nữ phụ ác độc từ trước đến nay đối với nam chính Tiêu Tịch Hàn chuyện xấu nào cũng làm, để tránh cho kết cục bi thảm, Nhan Xu quyết định dùng tình yêu của mẹ già làm ấm lòng Tiêu Tịch Hàn lạnh như băng", View = 0, story_image = "https://truyenvua.com/14393/1/1.jpg?gt=hdfgdfg&mobile=2", create_at = DateTime.Today, update_at = DateTime.Today, isComic = true },
                new Story { story_id = 11, story_name = "Cặp Vợ Chồng Hờ Ở Tiệm Cá Vàng", author_id = 10, description = "Câu chuyện về một cuộc hôn nhân không tình yêu ở một tiệm cá vàng. Còn bao chap sẽ yêu nhau thì không biết. Mong không có nhiều drama", View = 0, story_image = "https://i.truyenvua.com/ebook/190x247/cap-vo-chong-ho-o-tiem-ca-vang_1687656150.jpg?gt=hdfgdfg&mobile=2", create_at = DateTime.Today, update_at = DateTime.Today, isComic = true },
                new Story { story_id = 12, story_name = "Ressentiment", author_id = 10, description = "Takuro Sakamoto là một kẻ thất bại 30 tuổi vẫn sống với bố mẹ. Chán ngấy với công việc bế tắc và cuộc sống nói chung, anh bị một người bạn thuyết phục chơi thử trò chơi thực tế ảo. Nhưng ngay cả đối tác ảo của anh ấy là Tsukiko hay đơn giản là “Nguyệt” cũng rối rắm hơn gã tưởng.", View = 0, story_image = "https://i.truyenvua.com/ebook/190x247/ressentiment_1688627279.jpg?gt=hdfgdfg&mobile=2", create_at = DateTime.Today, update_at = DateTime.Today, isComic = true },
                new Story { story_id = 13, story_name = "Trường Học Wibu", author_id = 10, description = "Một ngôi trường toàn wibu - wibu muôn năm", View = 0, story_image = "", create_at = DateTime.Today, update_at = DateTime.Today, isComic = true },
                new Story { story_id = 14, story_name = "Black Clover - Thế Giới Phép Thuật", author_id = 10, description = "Aster và Yuno là hai đứa trẻ bị bỏ rơi ở nhà thờ và cùng nhau lớn lên tại đó. Khi còn nhỏ, chúng đã hứa với nhau xem ai sẽ trở thành Ma pháp vương tiếp theo. Thế nhưng, khi cả hai lớn lên, mọi sô chuyện đã thay đổi. Yuno là thiên tài ma pháp với sức mạnh tuyệt đỉnh trong khi Aster lại không thể sử dụng ma pháp và cố gắng bù đắp bằng thể lực", View = 0, story_image = "https://i.truyenvua.com/slider/290x191/slider_1560493497.jpg?gt=hdfgdfg&mobile=2", create_at = DateTime.Today, update_at = DateTime.Today, isComic = true },
                new Story { story_id = 15, story_name = "Thanh Gươm Diệt Quỷ", author_id = 10, description = "Kimetsu no Yaiba – Tanjirou là con cả của gia đình vừa mất cha. Một ngày nọ, Tanjirou đến thăm thị trấn khác để bán than, khi đêm về cậu ở nghỉ tại nhà người khác thay vì về nhà vì lời đồn thổi về ác quỷ luôn rình mò gần núi vào buổi tối. Khi cậu về nhà vào ngày hôm sau, bị kịch đang đợi chờ cậu…", View = 0, story_image = "https://image.api.playstation.com/vulcan/ap/rnd/202106/1704/2ZfAUG5CTXdM34S1OhmMW1zF.jpg", create_at = DateTime.Today, update_at = DateTime.Today, isComic = true },
                new Story { story_id = 16, story_name = "Thất Hình Đại Tội", author_id = 10, description = "Thất đại ác nhân, một nhóm chiến binh có tham vọng lật đổ vương quốc Britannia, được cho là đã bị tiêu diệt bởi các “hiệp sĩ thánh chiến” mặc dù vẫn còn 1 số người cho rằng họ vẫn còn sống. 10 năm sau, Các hiệp sĩ thánh chiến đã làm 1 cuộc đảo chính và khống chế đức vua, họ trở thành người cai trị độc tài mới của vương quốc", View = 0, story_image = "https://i.truyenvua.com/slider/290x191/slider_1559213426.jpg?gt=hdfgdfg&mobile=2", create_at = DateTime.Today, update_at = DateTime.Today, isComic = true },
                new Story { story_id = 17, story_name = "Va Phải Đại Boss", author_id = 10, description = "Truyện tranh Va Phải Đại Boss được cập nhật nhanh và đầy đủ nhất tại TruyenVN", View = 0, story_image = "https://i.truyenvua.com/ebook/190x247/va-phai-dai-boss_1686616982.jpg?gt=hdfgdfg&mobile=2", create_at = DateTime.Today, update_at = DateTime.Today, isComic = false },
                new Story { story_id = 18, story_name = "Kế Hoạch Trả Thù Của Đích Nữ Trọng Sinh", author_id = 10, description = "Đích nữ Bán Hạ bị chị kế và Thái tử ca ca mưu hại đến chết. Sau khi tỉnh lại, nàng trọng sinh đến thời điểm trước khi trở về kinh đô. Mang theo ký ức của kiếp trước, nàng đã lột bỏ làn da đẹp đẽ của người chị kế độc ác, và vạch trần bộ mặt bồ tát giả tạo của người mẹ kế đạo đức giả", View = 0, story_image = "https://i.truyenvua.com/ebook/190x247/ke-hoach-tra-thu-cua-dich-nu-trong-sinh_1686616863.jpg?gt=hdfgdfg&mobile=2", create_at = DateTime.Today, update_at = DateTime.Today, isComic = true },
                new Story { story_id = 19, story_name = "Cô Bé Quàng Khăn Đỏ Thịt Sói", author_id = 10, description = "Con sói dễ thương và đáng yêu, bị cô bé quàng khăn đỏ ăn thịt...!?\r\nSói Luka vốn định uy hiếp cô bé quàng khăn đỏ, nhưng chẳng biết vì sao lại bị cho rằng đang tỏ tình và bị ăn thịt.Luka luôn bị tấn công thế này, nhưng khi đêm trăng tròn thì...?", View = 0, story_image = "https://i.truyenvua.com/ebook/190x247/co-be-quang-khan-do-thit-soi_1686808465.jpg?gt=hdfgdfg&mobile=2", create_at = DateTime.Today, update_at = DateTime.Today, isComic = false },
                new Story { story_id = 20, story_name = "Vùng Đất Rộng Lớn", author_id = 10, description = "Câu truyện trước và sau của bộ phim \"King The Land\".\r\nNụ cười của thiên thần rạng rỡ nhất trần đời đã cứu rỗi lấy vị hoàng tử chúa ghét tiếng cười. Truyện kể về lần đầu tiên hai người gặp nhau và những câu chuyện thời thơ ấu.", View = 0, story_image = "https://i.truyenvua.com/ebook/190x247/vung-dat-rong-lon_1687076282.jpg?gt=hdfgdfg&mobile=2", create_at = DateTime.Today, update_at = DateTime.Today }

                );
            modelBuilder.Entity<Category>().HasData(
                new Category { cate_id = 1, cate_name = "Shounen", cate_description = "Nội dung của những bộ manga này thường liên quan đến đánh nhau và/hoặc bạo lực (ở mức bình thường, không thái quá)" },
                new Category { cate_id = 2, cate_name = "Fantasy", cate_description = "Thể loại xuất phát từ trí tưởng tượng phong phú, từ pháp thuật đến thế giới trong mơ thậm chí là những câu chuyện thần tiên" },
                new Category { cate_id = 3, cate_name = "Adventure", cate_description = "Thể loại phiêu lưu, mạo hiểm, thường là hành trình của các nhân vật" },
                new Category { cate_id = 4, cate_name = "Drama", cate_description = "Thể loại mang đến cho người xem những cảm xúc khác nhau: buồn bã, căng thẳng thậm chí là bi phẫn" },
                new Category { cate_id = 5, cate_name = "Comedy", cate_description = "Thể loại có nội dung trong sáng và cảm động, thường có các tình tiết gây cười, các xung đột nhẹ nhàng" },
                new Category { cate_id = 6, cate_name = "Action", cate_description = "Thể loại này thường có nội dung về đánh nhau, bạo lực, hỗn loạn, với diễn biến nhanh" },
                new Category { cate_id = 7, cate_name = "Romance", cate_description = "Thường là những câu chuyện về tình yêu, tình cảm lãng mạn." },
                new Category { cate_id = 8, cate_name = "Chuyển Sinh", cate_description = "Thể loại này là những câu chuyện về người ở một thế giới này xuyên đến một thế giới khác" },
                new Category { cate_id = 9, cate_name = "Manhua", cate_description = "Truyện tranh của Trung Quốc" },
                new Category { cate_id = 10, cate_name = "Ngôn Tình", cate_description = "Truyện thuộc kiểu lãng mạn, kể về những sự kiện vui buồn trong tình yêu của nhân vật chính." },
                new Category { cate_id = 11, cate_name = "Shoujo", cate_description = "Nội dung của những bộ manga này thường liên quan đến tình cảm lãng mạn, chú trọng đầu tư cho nhân vật (tính cách,...)" }
                );
            modelBuilder.Entity<User>().HasData(
                new User { user_id = 1, Email = "user@gmail.com", Password = "123", Name = "user", Gender = "Male", DateOfBirth = "12/12/2001", CreateAt = DateTime.Now, Role = 0 , isActive = true },
                new User { user_id = 2, Email = "admin@TruyenVn.com", Password = "123", Name = "admin", Gender = "Female", DateOfBirth = "22/07/2001", CreateAt = DateTime.Now, Role = 1, isActive = true }
                );
            modelBuilder.Entity<StoryCategory>().HasData(
                new StoryCategory { story_id = 1, cate_id = 1 },
                new StoryCategory { story_id = 1, cate_id = 2 },
                new StoryCategory { story_id = 1, cate_id = 3 },
                new StoryCategory { story_id = 1, cate_id = 5 },
                new StoryCategory { story_id = 2, cate_id = 1 },
                new StoryCategory { story_id = 2, cate_id = 2 },
                new StoryCategory { story_id = 2, cate_id = 4 },
                new StoryCategory { story_id = 3, cate_id = 1 },
                new StoryCategory { story_id = 3, cate_id = 3 },
                new StoryCategory { story_id = 3, cate_id = 6 },
                new StoryCategory { story_id = 3, cate_id = 2 },
                new StoryCategory { story_id = 4, cate_id = 2 },
                new StoryCategory { story_id = 4, cate_id = 3 },
                new StoryCategory { story_id = 4, cate_id = 1 },
                new StoryCategory { story_id = 4, cate_id = 5 },
                new StoryCategory { story_id = 5, cate_id = 1 },
                new StoryCategory { story_id = 5, cate_id = 3 },
                new StoryCategory { story_id = 5, cate_id = 5 },
                new StoryCategory { story_id = 6, cate_id = 1 },
                new StoryCategory { story_id = 6, cate_id = 3 },
                new StoryCategory { story_id = 6, cate_id = 2 },
                new StoryCategory { story_id = 6, cate_id = 9 },
                new StoryCategory { story_id = 7, cate_id = 1 },
                new StoryCategory { story_id = 7, cate_id = 3 },
                new StoryCategory { story_id = 7, cate_id = 5 },
                new StoryCategory { story_id = 8, cate_id = 1 },
                new StoryCategory { story_id = 8, cate_id = 3 },
                new StoryCategory { story_id = 8, cate_id = 5 },
                new StoryCategory { story_id = 8, cate_id = 9 },
                new StoryCategory { story_id = 9, cate_id = 5 },
                new StoryCategory { story_id = 9, cate_id = 4 },
                new StoryCategory { story_id = 9, cate_id = 11 },
                new StoryCategory { story_id = 10, cate_id = 11 },
                new StoryCategory { story_id = 10, cate_id = 10 },
                new StoryCategory { story_id = 10, cate_id = 7 },
                new StoryCategory { story_id = 11, cate_id = 7 },
                new StoryCategory { story_id = 11, cate_id = 10 },
                new StoryCategory { story_id = 11, cate_id = 5 },
                new StoryCategory { story_id = 12, cate_id = 9 },
                new StoryCategory { story_id = 12, cate_id = 10 },
                new StoryCategory { story_id = 12, cate_id = 11 },
                new StoryCategory { story_id = 13, cate_id = 5 },
                new StoryCategory { story_id = 13, cate_id = 4 },
                new StoryCategory { story_id = 13, cate_id = 7 },
                new StoryCategory { story_id = 14, cate_id = 1 },
                new StoryCategory { story_id = 14, cate_id = 2 },
                new StoryCategory { story_id = 14, cate_id = 3 },
                new StoryCategory { story_id = 15, cate_id = 2 },
                new StoryCategory { story_id = 15, cate_id = 3 },
                new StoryCategory { story_id = 15, cate_id = 6 },
                new StoryCategory { story_id = 16, cate_id = 2 },
                new StoryCategory { story_id = 16, cate_id = 3 },
                new StoryCategory { story_id = 16, cate_id = 6 },
                new StoryCategory { story_id = 17, cate_id = 9 },
                new StoryCategory { story_id = 17, cate_id = 10 },
                new StoryCategory { story_id = 17, cate_id = 11 },
                new StoryCategory { story_id = 18, cate_id = 4 },
                new StoryCategory { story_id = 18, cate_id = 8 },
                new StoryCategory { story_id = 18, cate_id = 10 },
                new StoryCategory { story_id = 18, cate_id = 11 },
                new StoryCategory { story_id = 19, cate_id = 7 },
                new StoryCategory { story_id = 19, cate_id = 10 },
                new StoryCategory { story_id = 19, cate_id = 11 },
                new StoryCategory { story_id = 20, cate_id = 3 },
                new StoryCategory { story_id = 20, cate_id = 4 },
                new StoryCategory { story_id = 20, cate_id = 5 }
                );
            modelBuilder.Entity<Report>().HasData(
                new Report { Id = 1, chapter_id = 1, user_id = 1, message = "OK", send_at = DateTime.Now }
                );
            modelBuilder.Entity<Viewed>().HasData(new Viewed { Id = 2, chapter_id = 2, user_id = 2, date_view = DateTime.Now });

            modelBuilder.Entity<Chapter>().HasData(
                new Chapter { chapter_id = 1, chapter_number = 1, story_id = 1, title = "Chapter 1", content = "https://res.cloudinary.com/dhboy15q8/image/upload/v1688894100/Fairy%20Tail/chapter1_itasyx.jpg", create_at= DateTime.Now , update_at = DateTime.Now},
                new Chapter { chapter_id = 2, chapter_number = 2, story_id = 1, title = "Chapter 2.1", content = "https://res.cloudinary.com/dhboy15q8/image/upload/v1688894100/Fairy%20Tail/chapter2.1_itasyx.jpg", create_at= DateTime.Now , update_at = DateTime.Now},
                new Chapter { chapter_id = 3, chapter_number = 3, story_id = 1, title = "Chapter 2.2", content = "https://res.cloudinary.com/dhboy15q8/image/upload/v1688894100/Fairy%20Tail/chapter2.2_itasyx.jpg", create_at= DateTime.Now , update_at = DateTime.Now},
                new Chapter { chapter_id = 4, chapter_number = 4, story_id = 1, title = "Chapter 3.1", content = "https://res.cloudinary.com/dhboy15q8/image/upload/v1688894100/Fairy%20Tail/chapter3.1_itasyx.jpg", create_at= DateTime.Now , update_at = DateTime.Now},
                new Chapter { chapter_id = 5, chapter_number = 5, story_id = 1, title = "Chapter 3.2", content = "https://res.cloudinary.com/dhboy15q8/image/upload/v1688894100/Fairy%20Tail/chapter3.2_itasyx.jpg", create_at= DateTime.Now , update_at = DateTime.Now},
                new Chapter { chapter_id = 6, chapter_number = 1, story_id = 2, title = "", content = "Error", create_at= DateTime.Now , update_at = DateTime.Now},
                new Chapter { chapter_id = 7, chapter_number = 2, story_id = 2, title = "Konohamaru", content = "https://res.cloudinary.com/dhboy15q8/image/upload/v1688895954/Naruto/chapter2.1_c5wrar.jpg", create_at= DateTime.Now , update_at = DateTime.Now},
                new Chapter { chapter_id = 8, chapter_number = 3, story_id = 2, title = "Konohamaru-next", content = "https://res.cloudinary.com/dhboy15q8/image/upload/v1688895954/Naruto/chapter2.2_c5wrar.jpg", create_at= DateTime.Now , update_at = DateTime.Now},
                new Chapter { chapter_id = 9, chapter_number = 4, story_id = 2, title = "Uchiha Sasuke", content = "https://res.cloudinary.com/dhboy15q8/image/upload/v1688895954/Naruto/chapter3.1_c5wrar.jpg", create_at= DateTime.Now , update_at = DateTime.Now},
                new Chapter { chapter_id = 10, chapter_number = 5, story_id = 2, title = "Uchiha Sasuke-next", content = "https://res.cloudinary.com/dhboy15q8/image/upload/v1688895954/Naruto/chapter3.2_c5wrar.jpg", create_at= DateTime.Now , update_at = DateTime.Now},
                new Chapter { chapter_id = 11, chapter_number = 1, story_id = 3, title = "Bắt đầu chuyến hành trình", content = "https://res.cloudinary.com/dhboy15q8/image/upload/v1688896729/One%20Piece/chapter1.1_qjkjcz.jpg", create_at= DateTime.Now , update_at = DateTime.Now},
                new Chapter { chapter_id = 12, chapter_number = 2, story_id = 3, title = "Bắt đầu chuyến hành trình - next", content = "https://res.cloudinary.com/dhboy15q8/image/upload/v1688896729/One%20Piece/chapter1.2_qjkjcz.jpg", create_at= DateTime.Now , update_at = DateTime.Now},
                new Chapter { chapter_id = 13, chapter_number = 3, story_id = 3, title = "Bắt đầu chuyến hành trình - next", content = "https://res.cloudinary.com/dhboy15q8/image/upload/v1688896729/One%20Piece/chapter1.3_qjkjcz.jpg", create_at= DateTime.Now , update_at = DateTime.Now},
                new Chapter { chapter_id = 14, chapter_number = 4, story_id = 3, title = "Chapter", content = "Error", create_at= DateTime.Now , update_at = DateTime.Now},
                new Chapter { chapter_id = 15, chapter_number = 5, story_id = 3, title = "Thợ săn hải tặc zoro", content = "https://res.cloudinary.com/dhboy15q8/image/upload/v1688897211/One%20Piece/chapter3.1_klvvkc.jpg", create_at= DateTime.Now , update_at = DateTime.Now},
                new Chapter { chapter_id = 16, chapter_number = 6, story_id = 3, title = "Thợ săn hải tặc zoro - next", content = "https://res.cloudinary.com/dhboy15q8/image/upload/v1688897211/One%20Piece/chapter3.2_klvvkc.jpg", create_at= DateTime.Now , update_at = DateTime.Now},
                new Chapter { chapter_id = 17, chapter_number = 1, story_id = 8, title = "Thiên tài rơi rụng", content = "https://res.cloudinary.com/dhboy15q8/raw/upload/v1688897810/%C4%90%C3%A2%CC%81u%20Pha%CC%81%20Th%C6%B0%C6%A1ng%20Khung/chapter1_lpk36g.json", create_at= DateTime.Now , update_at = DateTime.Now},
                new Chapter { chapter_id = 18, chapter_number = 2, story_id = 8, title = "Đấu khí đại lục", content = "https://res.cloudinary.com/dhboy15q8/raw/upload/v1688897951/%C4%90%C3%A2%CC%81u%20Pha%CC%81%20Th%C6%B0%C6%A1ng%20Khung/chapter2_rd9jwr.json", create_at= DateTime.Now , update_at = DateTime.Now},
                new Chapter { chapter_id = 19, chapter_number = 3, story_id = 8, title = "Khách nhân", content = "https://res.cloudinary.com/dhboy15q8/raw/upload/v1688897951/%C4%90%C3%A2%CC%81u%20Pha%CC%81%20Th%C6%B0%C6%A1ng%20Khung/chapter3_rd9jwr.json", create_at= DateTime.Now , update_at = DateTime.Now},
                new Chapter { chapter_id = 20, chapter_number = 4, story_id = 8, title = "Vân Lam tông", content = "https://res.cloudinary.com/dhboy15q8/raw/upload/v1688897951/%C4%90%C3%A2%CC%81u%20Pha%CC%81%20Th%C6%B0%C6%A1ng%20Khung/chapter4_rd9jwr.json", create_at= DateTime.Now , update_at = DateTime.Now},
                new Chapter { chapter_id = 21, chapter_number = 5, story_id = 8, title = "Tụ Khí Tán", content = "https://res.cloudinary.com/dhboy15q8/raw/upload/v1688897951/%C4%90%C3%A2%CC%81u%20Pha%CC%81%20Th%C6%B0%C6%A1ng%20Khung/chapter5_rd9jwr.json", create_at= DateTime.Now , update_at = DateTime.Now}
                );
        }

    }
}
