import SelectOptionsComponent from "./selectOptionsComponent.js";


function render(options)
{
    SelectOptionsComponent.render(options)
}

function renderForFilters(options)
{
    SelectOptionsComponent.renderForFilters(options)
}

function renderMoreOptions(dtos, container) 
{
    SelectOptionsComponent.renderMoreOptions(dtos, container);
}

const SelectOptionsController = {
    render : render,
    renderForFilters : renderForFilters,
    renderMoreOptions : renderMoreOptions
};

export default SelectOptionsController;