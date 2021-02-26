var myFullpage = new fullpage('#fullpage', {
	autoscrolling: false,
	navigation: true,
	controlArrows: false,
	anchors: ['homepage','about','sales','contact'],
	sectionsColor: ['gray', '#e9c46a', '#f4a261', '#e76f51']
});

setInterval(function() {
	fullpage_api.moveSlideRight();;
}, 3000);