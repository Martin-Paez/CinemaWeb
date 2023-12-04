function initDefaultTextAnimation(noSelectedValue, selectId, defaultLabel)
{
    let select = document.getElementById(selectId);
    let defaultOption = select.querySelector(`option[selected]`); 
    select.addEventListener('change', (event) => 
        {
            let option = event.target.selectedOptions[0];
            if(option.value == noSelectedValue)
                defaultOption.innerHTML = defaultLabel;
            else
                defaultOption.innerHTML = "Quitar Filtro";
        }
    );
}

function addDefaultValue(dtos, label, noSelectionValue)
{
    return [ 
        {
            id: noSelectionValue,
            label: label,
            selected : true
        },
        ... dtos
    ];
}

function getOption(item)
{
    return `<option ${item.selected ? "selected" : ""} value="${item.id}">${item.label}</option >`;
}

function getOptions(dtos)
{
    return `${
        dtos
            .map((item) => getOption(item))
            .join("")
    }`;
}

function getSelect(dtos, selectId, selectCssClassNames)
{
    return `
        <select id="${selectId}" class="${selectCssClassNames} form-select">
            ${getOptions(dtos)}
        </select>
    `;
}

function renderForFilters({
        dtos, 
        noSelectionValue,
        defaultLabel, 
        container,
        selectedOption,
        selectId,
        selectCssClassNames = "", 
    })
{
    render({
        dtos, 
        noSelectionValue,
        defaultLabel, 
        container,
        selectedOption,
        selectId,
        selectCssClassNames, 
    })
    initDefaultTextAnimation(noSelectionValue, selectId, defaultLabel);
}

function render({
    dtos, 
    container,
    selectId,
    noSelectionValue,
    selectedOption,
    defaultLabel = "Elija una opcion", 
    selectCssClassNames = "", 
})
{
    dtos = addDefaultValue(dtos, defaultLabel, noSelectionValue);
    if(selectedOption)
    {
        delete dtos[dtos.length-1].selected;
        let i=0;
        while(dtos[i].id != selectedOption && ++i<dtos.length);
        if(i < dtos.length)
            dtos[i].selected = true;
    }
    container.innerHTML += getSelect(dtos, selectId, selectCssClassNames);
}

function renderMoreOptions(dtos, container) 
{
    container.innerHTML += getOptions(dtos);
}

const SelectOptionsComponent = {
    render : render,
    renderForFilters : renderForFilters,
    renderMoreOptions : renderMoreOptions
};

export default SelectOptionsComponent;