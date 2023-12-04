import SelectOptionsController from "../selectOptionsComponent/selectOptionsController.js";

function render(container)
{
    container.innerHTML += ` 
        <div class="datePicker input-group">
            <div class="input-group mb-3">
                <label class="input-group-text" for="dateInput">Buscar por fecha:</label>
                <input id="dateInput" class="form-control" type="date" >
            </div>
            <div id="dateSuggestionsContainer" class="input-group">
                <div class="input-group mb-3">
                    <label class="input-group-text" for="dateSuggestionsSelect">Funciones sugeridas:</label>
                    <select class="form-select" id="dateSuggestionsSelect">
                    </select>
                </div>
            </div>
            <!--div style="border-bottom: 1px solid #000; width: 100%; margin-bottom: 1rem"></div-->
            
            
        </div>
        <p id="noFunctionsText" style="display: none; color: red;"></p>
    `;/*
    container = container.querySelector(".datePicker");
    SelectOptionsController.render({
        dtos : [], 
        container : container,
        selectId : "hourSelect",
        noSelectionValue : "",
        selectedOption : 0,
        defaultLabel : "Horario", 
    });*/
}

const DateSelectorComponent = {
    render : render
};

export default DateSelectorComponent;