
function invoiceNumber(minRange, maxRange)
{
    return Math.floor(Math.random() * (maxRange + 1) + minRange);


}


window.onload = function (){
    
    document.getElementById("yourInvoice").innerHTML = invoiceNumber(0, 10000000);
    $('#myInvoice').html(rand);
    return false;

};

    
   
    
