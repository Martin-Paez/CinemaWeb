function render(item, cardButtonLabel, container, cardClassNames)
{
    container.innerHTML += `
        <div data-aos="fade-right" class="card ${cardClassNames}">
            <a href="../../pages/movieDetails/movieDetails.html?movieId=${item.id}">
                <img src="${item.img}" class="contenedor-imagen card-img-top" alt="movie portrait">
            </a>
            <span class="etiqueta-esquina card-text btn btn-sm btn-warning fw-bold">${item.description}</span>   
            <div class="card-body" item-id=${item.id}>
                <!--span class="etiqueta-esquina card-text btn btn-sm btn-warning fw-bold">${item.description}</span-->   
                <h5 class="card-title">${item.title}</h5>
                <a href="#" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#buyModal">
                    ${cardButtonLabel}
                </a> 
            </div>
        </div>
    `;        
}

const CardComponent = {
    render : render
};

export default CardComponent;