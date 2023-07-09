var itemList = document.getElementsByClassName('items');
var count = 0;
axios.get('./theloai.json').then(function(response) {
    var render = response.data.map(function(x) {
        if (count % 2 === 0) {
            count++;
            return '<div class="row"><div> <a href="#">' + x.content + '</a></div>';
        } else {
            count++;
            return '<div> <a href="#">' + x.content + '</a></div></div>';
        }        
    }).join('');    
    itemList[0].innerHTML = render;     
});