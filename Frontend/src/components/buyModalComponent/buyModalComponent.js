import SelectOptionsController from "../selectOptionsComponent/selectOptionsController.js";
import DateSelectorController from "../dateSelector/dateSelectorController.js";

const state = {
    title : ""
}

function settitleTitle(title)
{
    state.title = title;
}

function getModal()
{
    return ` 
        <div class="modal fade" id="buyModal" tabindex="-1" aria-labelledby="buyModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog modal-dialog-centered modal-dialog-scrollable">
                <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="buyModalLabel">Conseguí tus Entradas para ver ${state.title}</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                    </div>
                    <div class="modal-footer">
                        <button id="buyButton" type="button" class="btn btn-success">Adquirir</button>
                        <p id="buyError"> No fue posible realizar la compra. Por favor intente con otra funcion. </p>
                    </div>
                </div>
            </div>
        </div>
    `;
}

function getImg()
{
    return `<img id="buyModalImg" src="https://www.cronista.com/files/image/294/294613/5ffe08a3d2cbc.jpg" alt=""></img>`;
}

function getInputs()
{
    return `
        <input id=user type="text" class="form-control mb-3" placeholder="Usuario" aria-label="Username" aria-describedby="basic-addon1">
        <input id=ticketAmount type="text" class="form-control" placeholder="Cantidad" aria-label="Username" aria-describedby="basic-addon1">
    `;                                 
}

function render(shows, date, container) 
{
    container.innerHTML += getModal();
    let modalBody = container.querySelector("#buyModal .modal-body");
    DateSelectorController.render(shows, date, modalBody);
    modalBody.innerHTML += getInputs();
    modalBody.innerHTML += getImg();
    let defaultOption = document.querySelectorAll("#dateSelector option[value='-1']")[0];     
}

const BuyModalComponent = {
    render : render,
    settitleTitle : settitleTitle
};

export default BuyModalComponent;