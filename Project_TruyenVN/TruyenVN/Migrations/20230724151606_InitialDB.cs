using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruyenVN.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    author_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    author_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    author_description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.author_id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    cate_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cate_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    cate_description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.cate_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    DateOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "Stories",
                columns: table => new
                {
                    story_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    story_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(700)", maxLength: 700, nullable: false),
                    View = table.Column<int>(type: "int", nullable: false),
                    author_id = table.Column<int>(type: "int", nullable: false),
                    isComic = table.Column<bool>(type: "bit", nullable: false),
                    story_image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stories", x => x.story_id);
                    table.ForeignKey(
                        name: "FK_Stories_Authors_author_id",
                        column: x => x.author_id,
                        principalTable: "Authors",
                        principalColumn: "author_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chapters",
                columns: table => new
                {
                    chapter_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    chapter_number = table.Column<int>(type: "int", nullable: false),
                    story_id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapters", x => x.chapter_id);
                    table.ForeignKey(
                        name: "FK_Chapters_Stories_story_id",
                        column: x => x.story_id,
                        principalTable: "Stories",
                        principalColumn: "story_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoryCategories",
                columns: table => new
                {
                    story_id = table.Column<int>(type: "int", nullable: false),
                    cate_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryCategories", x => new { x.cate_id, x.story_id });
                    table.ForeignKey(
                        name: "FK_StoryCategories_Categories_cate_id",
                        column: x => x.cate_id,
                        principalTable: "Categories",
                        principalColumn: "cate_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoryCategories_Stories_story_id",
                        column: x => x.story_id,
                        principalTable: "Stories",
                        principalColumn: "story_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    chapter_id = table.Column<int>(type: "int", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    send_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Chapters_chapter_id",
                        column: x => x.chapter_id,
                        principalTable: "Chapters",
                        principalColumn: "chapter_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vieweds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    chapter_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    date_view = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vieweds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vieweds_Chapters_chapter_id",
                        column: x => x.chapter_id,
                        principalTable: "Chapters",
                        principalColumn: "chapter_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vieweds_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "author_id", "author_description", "author_name" },
                values: new object[,]
                {
                    { 1, "Mashima Hiro là một họa sĩ manga người Nhật nổi tiếng với ba bộ truyện Rave Master, Fairy Tail và Edens Zero.", "Mashima Hiro" },
                    { 2, "Kishimoto Masashi là một họa sĩ truyện tranh đã được biết đến qua bộ truyện tranh nổi tiếng thế giới Naruto", "Kishimoto Masashi" },
                    { 3, "Oda Eiichirō là một họa sĩ truyện tranh người Nhật Bản, hiện đang sáng tác cho nhà xuất bản Shūeisha", "Oda Eiichiro" },
                    { 4, "Atsuo Ueda là trợ lí cho họa sĩ Mashima Hiro", "Atsuo Ueda" },
                    { 5, "Fujiko Fujio là bút danh chung của hai nghệ sĩ manga Nhật Bản, người Việt gọi là Ông Hai Phú hay Ông Phú Sĩ.", "Fujiko Fujio" },
                    { 6, "Trương Uy, là một tiểu thuyết gia, văn học mạng người Trung Quốc, được biết đến với bút danh Đường Gia Tam Thiếu.", "Đường Gia Tam Thiếu" },
                    { 7, "Chưa có thông tin", "Umemura Shinya" },
                    { 8, "Ông là một mangaka nổi tiếng với các tác phẩm như Yaiba, Magic Kaito hay Thám tử lừng danh Conan.", "Aoyama Gōshō" },
                    { 9, "Anh bắt đầu viết tiểu thuyết khi còn mới là học sinh trung học và dần trở thành một trong những cây bút tiêu biểu của văn học mạng Trung Quốc", "Thiên Tằm Thổ Đậu" },
                    { 10, "Đang cập nhật", "Đang cập nhật" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "cate_id", "cate_description", "cate_name" },
                values: new object[,]
                {
                    { 1, "Nội dung của những bộ manga này thường liên quan đến đánh nhau và/hoặc bạo lực (ở mức bình thường, không thái quá)", "Shounen" },
                    { 2, "Thể loại xuất phát từ trí tưởng tượng phong phú, từ pháp thuật đến thế giới trong mơ thậm chí là những câu chuyện thần tiên", "Fantasy" },
                    { 3, "Thể loại phiêu lưu, mạo hiểm, thường là hành trình của các nhân vật", "Adventure" },
                    { 4, "Thể loại mang đến cho người xem những cảm xúc khác nhau: buồn bã, căng thẳng thậm chí là bi phẫn", "Drama" },
                    { 5, "Thể loại có nội dung trong sáng và cảm động, thường có các tình tiết gây cười, các xung đột nhẹ nhàng", "Comedy" },
                    { 6, "Thể loại này thường có nội dung về đánh nhau, bạo lực, hỗn loạn, với diễn biến nhanh", "Action" },
                    { 7, "Thường là những câu chuyện về tình yêu, tình cảm lãng mạn.", "Romance" },
                    { 8, "Thể loại này là những câu chuyện về người ở một thế giới này xuyên đến một thế giới khác", "Chuyển Sinh" },
                    { 9, "Truyện tranh của Trung Quốc", "Manhua" },
                    { 10, "Truyện thuộc kiểu lãng mạn, kể về những sự kiện vui buồn trong tình yêu của nhân vật chính.", "Ngôn Tình" },
                    { 11, "Nội dung của những bộ manga này thường liên quan đến tình cảm lãng mạn, chú trọng đầu tư cho nhân vật (tính cách,...)", "Shoujo" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "user_id", "CreateAt", "DateOfBirth", "Email", "Gender", "Name", "Password", "Role", "isActive" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8353), "12/12/2001", "user@gmail.com", "Male", "user", "123", 0, true },
                    { 2, new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8355), "22/07/2001", "admin@TruyenVn.com", "Female", "admin", "123", 1, true }
                });

            migrationBuilder.InsertData(
                table: "Stories",
                columns: new[] { "story_id", "View", "author_id", "create_at", "description", "isComic", "story_image", "story_name", "update_at" },
                values: new object[,]
                {
                    { 1, 50, 1, new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local), "Xuyên suốt câu chuyện là cuộc phiêu lưu của một Sorceress tên là Lucy Heartphilia, sau khi cô tham gia vào hiệp hội hội Fairy Tail, cô đã cùng với Natsu Dragneel hành trình để đi tìm một con rồng tên là Igneel.", true, "https://news-w.com/wp-content/uploads/2022/03/ve-hoi-phap-su-thumbnail.jpg", "Hội pháp sư - Fairy Tail", new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 2, 30, 2, new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local), "Câu chuyện xoay quanh Uzumaki Naruto, một ninja trẻ muốn tìm cách khẳng định mình để được mọi người công nhận và nuôi ước mơ trở thành Hokage - người lãnh đạo Làng Lá. ", true, "https://upload.wikimedia.org/wikipedia/en/9/94/NarutoCoverTankobon1.jpg", "Naruto", new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 3, 28, 3, new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local), ". One Piece kể về cuộc hành trình của Monkey D. Luffy - thuyền trưởng của băng hải tặc Mũ Rơm và các đồng đội của cậu. Luffy tìm kiếm vùng biển bí ẩn nơi cất giữ kho báu lớn nhất thế giới One Piece, với mục tiêu trở thành Vua Hải Tặc.", true, "https://i.truyenvua.com/slider/290x191/slider_1559211185.jpg?gt=hdfgdfg&mobile=2", "Đảo Hải Tặc - One Piece", new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 4, 0, 4, new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local), "Phần tiếp theo của loạt truyện đình đám cùng tên khi nhóm Natsu đi làm nhiệm vụ trăm năm", true, "https://i.truyenvua.com/slider/290x191/slider_1561609693.jpg?gt=hdfgdfg&mobile=2", "Fairy Tail - Nhiệm vụ trăm năm", new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 5, 12, 5, new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local), "Trong truyện lấy bối cảnh ở thế kỷ 22, Doraemon là chú mèo robot của tương lai do xưởng Matsushiba — công xưởng chuyên sản xuất robot vốn dĩ nhằm mục đích chăm sóc trẻ nhỏ.", true, "https://vcdn.tikicdn.com/cache/450x450//media/catalog/product/i/m/img225_11.jpg", "Doraemon", new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 6, 0, 6, new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local), "câu chuyện về Đường Tam, một thiên tài chế tạo ám khí của đường môn đã xuyên không đến Đấu La Đại Lục.", false, "https://hhhkungfu.tv/wp-content/uploads/Dau-La-Dai-Luc-2.jpg", "Đấu la đại lục", new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 7, 0, 7, new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local), "Truyện xoay quanh một cậu thám tử trung học có tên là Kudo Shinichi trong lúc đang điều tra một Tổ chức Áo đen bí ẩn đã bị ép phải uống một loại thuốc độc có thể gây chết người. May mắn là cậu đã sống sót nhưng cơ thể thì lại bị teo nhỏ như một đứa bé 6 tuổi. Kể từ đó để tránh bị lộ thân phận thực sự của mình, cậu đã lấy tên là Edogawa Conan và chuyển đến sống ở nhà của cô bạn thời thơ ấu Mori Ran cùng với bố của cô ấy là một thám tử tư tên Mori Kogoro với hy vọng một ngày nào đó cậu có thể hạ gục Tổ chức Áo Đen và lấy lại hình dáng ban đầu.", true, "https://im.uniqlo.com/global-cms/spa/rese7e57a44a646f56f9cf35532a9777337fr.jpg", "Conan", new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 8, 0, 8, new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local), "Đấu Phá Thương Khung là một câu chuyện huyền huyễn đặc sắc kể về Tiêu Viêm, một thiên chi kiêu tử với thiên phú tu luyện mà ai ai cũng hâm mộ, bỗng một ngày người mẹ mất đi đễ lại di vật là một chiếc giới chỉ màu đen nhưng từ khi đó Tiêu Viêm đã mất đi thiên phú tu luyện của mình.\r\n\r\n- Từ thiên tài rớt xuống làm phế vật trong 3 năm, rồi bị vị hôn thê thẳng thừng từ hôn, làm dấy lên ý chí nam nhi của mình, Tiêu Viêm nhờ di vật của mẫu thân để lại là 1 chiếc hắc giới chỉ (nhẫn màu đen)Tiêu Viêm gặp được hồn của Dược Lão (Dược Trần – Dược tôn giả) 1 đại luyện dược tông sư của đấu khí đại lục…", false, "https://img.meta.com.vn/Data/image/2021/11/01/lich-chieu-dau-pha-khung-thuong-ova-3.jpg", "Đấu phá thương khung", new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 9, 0, 10, new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local), "“Hãy chia tay với tên khốn đó đi”\r\nCái gì? Cậu là bạn của tôi đó…!\r\n“Sang Jae Soo” người muốn tôi và người yêu chia tay. Và tôi, người không thể đẩy cậu ấy ra xa, ‘Ha Won Soo.", true, "https://i.truyenvua.com/ebook/190x247/that-dang-thuong-cho-chung-toi_1688092650.jpg?gt=hdfgdfg&mobile=2", "Thật Đáng Thương Cho Chúng Tôi", new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 10, 0, 10, new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local), "Nhan Xu một thời xuyên qua trên người nữ phụ độc ác của tiểu thuyết nam tần, nữ phụ ác độc từ trước đến nay đối với nam chính Tiêu Tịch Hàn chuyện xấu nào cũng làm, để tránh cho kết cục bi thảm, Nhan Xu quyết định dùng tình yêu của mẹ già làm ấm lòng Tiêu Tịch Hàn lạnh như băng", true, "https://truyenvua.com/14393/1/1.jpg?gt=hdfgdfg&mobile=2", "Mở Đầu Ta Ngược Đãi Nam Chủ", new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 11, 0, 10, new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local), "Câu chuyện về một cuộc hôn nhân không tình yêu ở một tiệm cá vàng. Còn bao chap sẽ yêu nhau thì không biết. Mong không có nhiều drama", true, "https://i.truyenvua.com/ebook/190x247/cap-vo-chong-ho-o-tiem-ca-vang_1687656150.jpg?gt=hdfgdfg&mobile=2", "Cặp Vợ Chồng Hờ Ở Tiệm Cá Vàng", new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 12, 0, 10, new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local), "Takuro Sakamoto là một kẻ thất bại 30 tuổi vẫn sống với bố mẹ. Chán ngấy với công việc bế tắc và cuộc sống nói chung, anh bị một người bạn thuyết phục chơi thử trò chơi thực tế ảo. Nhưng ngay cả đối tác ảo của anh ấy là Tsukiko hay đơn giản là “Nguyệt” cũng rối rắm hơn gã tưởng.", true, "https://i.truyenvua.com/ebook/190x247/ressentiment_1688627279.jpg?gt=hdfgdfg&mobile=2", "Ressentiment", new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 13, 0, 10, new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local), "Một ngôi trường toàn wibu - wibu muôn năm", true, "", "Trường Học Wibu", new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 14, 0, 10, new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local), "Aster và Yuno là hai đứa trẻ bị bỏ rơi ở nhà thờ và cùng nhau lớn lên tại đó. Khi còn nhỏ, chúng đã hứa với nhau xem ai sẽ trở thành Ma pháp vương tiếp theo. Thế nhưng, khi cả hai lớn lên, mọi sô chuyện đã thay đổi. Yuno là thiên tài ma pháp với sức mạnh tuyệt đỉnh trong khi Aster lại không thể sử dụng ma pháp và cố gắng bù đắp bằng thể lực", true, "https://i.truyenvua.com/slider/290x191/slider_1560493497.jpg?gt=hdfgdfg&mobile=2", "Black Clover - Thế Giới Phép Thuật", new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 15, 0, 10, new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local), "Kimetsu no Yaiba – Tanjirou là con cả của gia đình vừa mất cha. Một ngày nọ, Tanjirou đến thăm thị trấn khác để bán than, khi đêm về cậu ở nghỉ tại nhà người khác thay vì về nhà vì lời đồn thổi về ác quỷ luôn rình mò gần núi vào buổi tối. Khi cậu về nhà vào ngày hôm sau, bị kịch đang đợi chờ cậu…", true, "https://image.api.playstation.com/vulcan/ap/rnd/202106/1704/2ZfAUG5CTXdM34S1OhmMW1zF.jpg", "Thanh Gươm Diệt Quỷ", new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 16, 0, 10, new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local), "Thất đại ác nhân, một nhóm chiến binh có tham vọng lật đổ vương quốc Britannia, được cho là đã bị tiêu diệt bởi các “hiệp sĩ thánh chiến” mặc dù vẫn còn 1 số người cho rằng họ vẫn còn sống. 10 năm sau, Các hiệp sĩ thánh chiến đã làm 1 cuộc đảo chính và khống chế đức vua, họ trở thành người cai trị độc tài mới của vương quốc", true, "https://i.truyenvua.com/slider/290x191/slider_1559213426.jpg?gt=hdfgdfg&mobile=2", "Thất Hình Đại Tội", new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 17, 0, 10, new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local), "Truyện tranh Va Phải Đại Boss được cập nhật nhanh và đầy đủ nhất tại TruyenVN", false, "https://i.truyenvua.com/ebook/190x247/va-phai-dai-boss_1686616982.jpg?gt=hdfgdfg&mobile=2", "Va Phải Đại Boss", new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 18, 0, 10, new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local), "Đích nữ Bán Hạ bị chị kế và Thái tử ca ca mưu hại đến chết. Sau khi tỉnh lại, nàng trọng sinh đến thời điểm trước khi trở về kinh đô. Mang theo ký ức của kiếp trước, nàng đã lột bỏ làn da đẹp đẽ của người chị kế độc ác, và vạch trần bộ mặt bồ tát giả tạo của người mẹ kế đạo đức giả", true, "https://i.truyenvua.com/ebook/190x247/ke-hoach-tra-thu-cua-dich-nu-trong-sinh_1686616863.jpg?gt=hdfgdfg&mobile=2", "Kế Hoạch Trả Thù Của Đích Nữ Trọng Sinh", new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 19, 0, 10, new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local), "Con sói dễ thương và đáng yêu, bị cô bé quàng khăn đỏ ăn thịt...!?\r\nSói Luka vốn định uy hiếp cô bé quàng khăn đỏ, nhưng chẳng biết vì sao lại bị cho rằng đang tỏ tình và bị ăn thịt.Luka luôn bị tấn công thế này, nhưng khi đêm trăng tròn thì...?", false, "https://i.truyenvua.com/ebook/190x247/co-be-quang-khan-do-thit-soi_1686808465.jpg?gt=hdfgdfg&mobile=2", "Cô Bé Quàng Khăn Đỏ Thịt Sói", new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 20, 0, 10, new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local), "Câu truyện trước và sau của bộ phim \"King The Land\".\r\nNụ cười của thiên thần rạng rỡ nhất trần đời đã cứu rỗi lấy vị hoàng tử chúa ghét tiếng cười. Truyện kể về lần đầu tiên hai người gặp nhau và những câu chuyện thời thơ ấu.", false, "https://i.truyenvua.com/ebook/190x247/vung-dat-rong-lon_1687076282.jpg?gt=hdfgdfg&mobile=2", "Vùng Đất Rộng Lớn", new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Local) }
                });

            migrationBuilder.InsertData(
                table: "Chapters",
                columns: new[] { "chapter_id", "chapter_number", "content", "create_at", "story_id", "title", "update_at" },
                values: new object[,]
                {
                    { 1, 1, "https://res.cloudinary.com/dhboy15q8/image/upload/v1688894100/Fairy%20Tail/chapter1_itasyx.jpg", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8469), 1, "Chapter 1", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8469) },
                    { 2, 2, "https://res.cloudinary.com/dhboy15q8/image/upload/v1688894100/Fairy%20Tail/chapter2.1_itasyx.jpg", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8471), 1, "Chapter 2.1", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8472) },
                    { 3, 3, "https://res.cloudinary.com/dhboy15q8/image/upload/v1688894100/Fairy%20Tail/chapter2.2_itasyx.jpg", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8473), 1, "Chapter 2.2", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8473) },
                    { 4, 4, "https://res.cloudinary.com/dhboy15q8/image/upload/v1688894100/Fairy%20Tail/chapter3.1_itasyx.jpg", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8474), 1, "Chapter 3.1", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8475) },
                    { 5, 5, "https://res.cloudinary.com/dhboy15q8/image/upload/v1688894100/Fairy%20Tail/chapter3.2_itasyx.jpg", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8475), 1, "Chapter 3.2", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8476) },
                    { 6, 1, "Error", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8477), 2, "", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8477) },
                    { 7, 2, "https://res.cloudinary.com/dhboy15q8/image/upload/v1688895954/Naruto/chapter2.1_c5wrar.jpg", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8479), 2, "Konohamaru", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8479) },
                    { 8, 3, "https://res.cloudinary.com/dhboy15q8/image/upload/v1688895954/Naruto/chapter2.2_c5wrar.jpg", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8481), 2, "Konohamaru-next", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8481) },
                    { 9, 4, "https://res.cloudinary.com/dhboy15q8/image/upload/v1688895954/Naruto/chapter3.1_c5wrar.jpg", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8482), 2, "Uchiha Sasuke", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8482) },
                    { 10, 5, "https://res.cloudinary.com/dhboy15q8/image/upload/v1688895954/Naruto/chapter3.2_c5wrar.jpg", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8483), 2, "Uchiha Sasuke-next", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8484) },
                    { 11, 1, "https://res.cloudinary.com/dhboy15q8/image/upload/v1688896729/One%20Piece/chapter1.1_qjkjcz.jpg", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8484), 3, "Bắt đầu chuyến hành trình", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8485) },
                    { 12, 2, "https://res.cloudinary.com/dhboy15q8/image/upload/v1688896729/One%20Piece/chapter1.2_qjkjcz.jpg", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8486), 3, "Bắt đầu chuyến hành trình - next", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8486) },
                    { 13, 3, "https://res.cloudinary.com/dhboy15q8/image/upload/v1688896729/One%20Piece/chapter1.3_qjkjcz.jpg", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8487), 3, "Bắt đầu chuyến hành trình - next", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8487) },
                    { 14, 4, "Error", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8488), 3, "Chapter", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8489) },
                    { 15, 5, "https://res.cloudinary.com/dhboy15q8/image/upload/v1688897211/One%20Piece/chapter3.1_klvvkc.jpg", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8490), 3, "Thợ săn hải tặc zoro", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8490) },
                    { 16, 6, "https://res.cloudinary.com/dhboy15q8/image/upload/v1688897211/One%20Piece/chapter3.2_klvvkc.jpg", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8491), 3, "Thợ săn hải tặc zoro - next", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8491) },
                    { 17, 1, "https://res.cloudinary.com/dhboy15q8/raw/upload/v1688897810/%C4%90%C3%A2%CC%81u%20Pha%CC%81%20Th%C6%B0%C6%A1ng%20Khung/chapter1_lpk36g.json", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8492), 8, "Thiên tài rơi rụng", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8492) },
                    { 18, 2, "https://res.cloudinary.com/dhboy15q8/raw/upload/v1688897951/%C4%90%C3%A2%CC%81u%20Pha%CC%81%20Th%C6%B0%C6%A1ng%20Khung/chapter2_rd9jwr.json", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8494), 8, "Đấu khí đại lục", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8494) },
                    { 19, 3, "https://res.cloudinary.com/dhboy15q8/raw/upload/v1688897951/%C4%90%C3%A2%CC%81u%20Pha%CC%81%20Th%C6%B0%C6%A1ng%20Khung/chapter3_rd9jwr.json", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8495), 8, "Khách nhân", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8495) },
                    { 20, 4, "https://res.cloudinary.com/dhboy15q8/raw/upload/v1688897951/%C4%90%C3%A2%CC%81u%20Pha%CC%81%20Th%C6%B0%C6%A1ng%20Khung/chapter4_rd9jwr.json", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8496), 8, "Vân Lam tông", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8497) },
                    { 21, 5, "https://res.cloudinary.com/dhboy15q8/raw/upload/v1688897951/%C4%90%C3%A2%CC%81u%20Pha%CC%81%20Th%C6%B0%C6%A1ng%20Khung/chapter5_rd9jwr.json", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8497), 8, "Tụ Khí Tán", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8498) }
                });

            migrationBuilder.InsertData(
                table: "StoryCategories",
                columns: new[] { "cate_id", "story_id" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 5 },
                    { 1, 6 },
                    { 1, 7 },
                    { 1, 8 },
                    { 1, 14 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 2, 6 },
                    { 2, 14 },
                    { 2, 15 },
                    { 2, 16 },
                    { 3, 1 },
                    { 3, 3 },
                    { 3, 4 },
                    { 3, 5 }
                });

            migrationBuilder.InsertData(
                table: "StoryCategories",
                columns: new[] { "cate_id", "story_id" },
                values: new object[,]
                {
                    { 3, 6 },
                    { 3, 7 },
                    { 3, 8 },
                    { 3, 14 },
                    { 3, 15 },
                    { 3, 16 },
                    { 3, 20 },
                    { 4, 2 },
                    { 4, 9 },
                    { 4, 13 },
                    { 4, 18 },
                    { 4, 20 },
                    { 5, 1 },
                    { 5, 4 },
                    { 5, 5 },
                    { 5, 7 },
                    { 5, 8 },
                    { 5, 9 },
                    { 5, 11 },
                    { 5, 13 },
                    { 5, 20 },
                    { 6, 3 },
                    { 6, 15 },
                    { 6, 16 },
                    { 7, 10 },
                    { 7, 11 },
                    { 7, 13 },
                    { 7, 19 },
                    { 8, 18 },
                    { 9, 6 },
                    { 9, 8 },
                    { 9, 12 },
                    { 9, 17 },
                    { 10, 10 },
                    { 10, 11 },
                    { 10, 12 },
                    { 10, 17 },
                    { 10, 18 },
                    { 10, 19 },
                    { 11, 9 },
                    { 11, 10 },
                    { 11, 12 }
                });

            migrationBuilder.InsertData(
                table: "StoryCategories",
                columns: new[] { "cate_id", "story_id" },
                values: new object[] { 11, 17 });

            migrationBuilder.InsertData(
                table: "StoryCategories",
                columns: new[] { "cate_id", "story_id" },
                values: new object[] { 11, 18 });

            migrationBuilder.InsertData(
                table: "StoryCategories",
                columns: new[] { "cate_id", "story_id" },
                values: new object[] { 11, 19 });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "Id", "chapter_id", "message", "send_at", "user_id" },
                values: new object[] { 1, 1, "OK", new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8446), 1 });

            migrationBuilder.InsertData(
                table: "Vieweds",
                columns: new[] { "Id", "chapter_id", "date_view", "user_id" },
                values: new object[] { 2, 2, new DateTime(2023, 7, 24, 22, 16, 6, 566, DateTimeKind.Local).AddTicks(8457), 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_story_id",
                table: "Chapters",
                column: "story_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_chapter_id",
                table: "Reports",
                column: "chapter_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_user_id",
                table: "Reports",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_author_id",
                table: "Stories",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_StoryCategories_story_id",
                table: "StoryCategories",
                column: "story_id");

            migrationBuilder.CreateIndex(
                name: "IX_Vieweds_chapter_id",
                table: "Vieweds",
                column: "chapter_id");

            migrationBuilder.CreateIndex(
                name: "IX_Vieweds_user_id",
                table: "Vieweds",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "StoryCategories");

            migrationBuilder.DropTable(
                name: "Vieweds");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Chapters");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Stories");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
