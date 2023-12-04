import BuyModalComponent from "./buyModalComponent.js";
import ShowRepository from "../../api/show/showRepository.js";


async function render(shows, date, container)
{
    BuyModalComponent.render(shows, date, container);
    document
        .getElementById("buyButton")
        .addEventListener('click', async ()=>{
            let user = document.getElementById("user").value;
            let amount = document.getElementById("ticketAmount").value;
            
            let selectElement = document.getElementById("dateSuggestionsSelect")
            var selectedIndex = selectElement.selectedIndex;
            var showId = selectElement.options[selectedIndex].value;
            let result = await ShowRepository.addShowTickets(showId, {usuario:user, cantidad:amount});
            if(result === null)
            {
                document.getElementById("buyError").style.display = "block";
            }
            else
            {
                Swal.fire({
                    title: "Gracias Por Su compra",
                    text: '',
                    icon: 'success',  
                    iconColor: '#4CAF50', 
                    confirmButtonText: 'OK',
                    confirmButtonColor: '#4CAF50',
                  }).then((result) => {
                    if (result.isConfirmed) {
                        window.location.reload();
                    } 
                  });
                
            }
        })
}

function setMovieTitle(title) {
    BuyModalComponent.settitleTitle(title);
}

const BuyModalController = {
    render : render,
    setMovieTitle : setMovieTitle
};

export default BuyModalController;