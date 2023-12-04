import CardComponent from "./cardComponent.js";

function render(item, cardButtonLabel, container, cardClassNames)
{
    CardComponent.render(item, cardButtonLabel, container, cardClassNames);
}

const CardController = {
    render : render
};

export default CardController;