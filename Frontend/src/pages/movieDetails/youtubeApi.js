async function renderVideo(videoId) {
    var iframe = document.createElement("iframe");
    iframe.setAttribute("width", "100%");
    iframe.setAttribute("height", "100%");
    iframe.setAttribute("src", "https://www.youtube.com/embed/" + videoId);
    iframe.setAttribute("frameborder", "0");
    iframe.setAttribute("allowfullscreen", "");
    document.getElementById("youtubePlayer").innerHTML = "";
    document.getElementById("youtubePlayer").appendChild(iframe);
    $("#trailerModal").on("show.bs.modal", async function () {
      await renderVideo(videoId);
    });
  }
  
  async function render(youtubeURL) {
    let videoId = getVideoId(youtubeURL)
    var tag = document.createElement("script");
    tag.src = "https://www.youtube.com/iframe_api";
    var firstScriptTag = document.getElementsByTagName("script")[0];
    firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
  
    window.onYouTubeIframeAPIReady = async function () {
      await renderVideo(videoId);
    };
  }
  
  function getVideoId(youtubeURL){
    const params = new URLSearchParams(new URL(youtubeURL).search);
    const videoId = params.get("v");
    return videoId;
  }
  
  const YoutubeAPI = {
    render: render,
  };
  
  export default YoutubeAPI;
  