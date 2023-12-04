function generateURL(apiUrl, endpoint) 
{
    return `${apiUrl}${endpoint}`;
}

function defaultOptions(
  method, 
  data = null, 
  contentType = "application/json"
  ) 
{
    if (!Object.values(HttpMethod).includes(method))
      throw new Error("Método HTTP no válido");
  
    return {
      method: method,
      headers: { "Content-Type": contentType },
      mode: "cors",
      cache: "default",
      body: data ? JSON.stringify(data) : null,
    };
}

async function safeFetch(url, options) 
{
    let result
    try {
      result = await fetch(url, options);
    } catch (error) {
      let msg = "Comunication error ->";
      msg += `URL: {url} |`;
      msg += `Options: {options}.`;
      console.error(msg, error);
    }
    return result;
}

async function defaultApiResponseManager(result)
{
    return (result && result.ok) ? result.json() : null;
}

const HttpMethod = {
  GET: "GET",
  POST: "POST",
  PUT: "PUT",
  DELET: "DELETE",
  PATCH: "PATCH",
};

export {
    safeFetch,
    defaultOptions,
    generateURL,
    defaultApiResponseManager,
    HttpMethod
};