$(document).ready(() => {
    getRandom();
});

const getRandom = () => {
    apiGet('random', (message) => {
        $('#message_text').text(message.text);
    });
}

const copyToClipboard = () => {
    var text = document.getElementById('message_text').innerText;
    var elem = document.createElement("textarea");
    document.body.appendChild(elem);
    elem.value = text;
    elem.select();
    document.execCommand("copy");
    document.body.removeChild(elem);
}


