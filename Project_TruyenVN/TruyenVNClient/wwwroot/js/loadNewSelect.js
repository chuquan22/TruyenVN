var dataList = document.getElementsByClassName('theloai');
axios.get('./theloai.json').then(function(response) {
    var render = response.data.map(function(x) {
        return '<option value="' + x.content + '">' + x.content + '</option>';
    }).join('');
    for (element of dataList) {
        element.innerHTML = render;
    }    
});