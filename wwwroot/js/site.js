let index = 0;

// add tag to list
function AddTag() {
    // get a reference to the tagValues input element
    var tagEntry = document.getElementById("TagEntry");

    // use search function to help detext error state
    let searchResult = search(tagEntry.value);

    if (searchResult != null) {
        // trigger sweet alert for whatever condition is contained in searchResult
        swalWithDarkButton.fire({
            html: `<span class='font-weight-bolder'>${searchResult.toUpperCase()}</span>`
        });
    }
    else {
        // no error
        // create a new select option
        let newOption = new Option(tagEntry.value, tagEntry.value);
        document.getElementById("TagList").options[index++] = newOption;
    }


    // clear out tagentry control
    tagEntry.value = "";

    return true;
}


//
function DeleteTag() {
    let tagCount = 1;

    let tagList = document.getElementById("TagList");

    if (!tagList) return false;

    if (tagList.selectedIndex == -1) {
        swalWithDarkButton.fire({
            html: "<span class='font-weight-bolder'>CHOOSE A TAG BEGORE DELETING</span>"
        });

        return true;
    }

    while (tagCount > 0) {
        if (tagList.selectedIndex >= 0) {
            tagList.options[tagList.selectedIndex] = null;

            --tagCount;
        }
        else
            tagCount = 0;

        index--;
    }
}


$("form").on("submit", function () {
    $("#TagList option").prop("selected", "selected");
})


// look for tagValues variable to see if it has data
if (tagValues != '') {
    let tagArray = tagValues.split(",");
    

    for (let loop = 0; loop < tagArray.length; loop++) {
        // load up or replace the options that we have
        ReplaceTag(tagArray[loop], loop);
        index++;
    }
}

function ReplaceTag(tag, index) {
    let newOption = new Option(tag, tag);
    document.getElementById("TagList").options[index] = newOption;
}


// sweet alert
// search function detect eithere an empty or a duplicate tag
// and return an error string if an error is dectected
function search(str) {
    if (str == "") {
        return 'Empty tags are not permitted';
    }

    let tagsEl = document.getElementById('TagList');
    if (tagsEl) {
        let options = tagsEl.options;

        for (let i = 0; i < options.length; i++) {
            if (options[i].value == str)
                return `The Tag #${str} is a duplicate tag and is not permitted`;
        }
    }
}


const swalWithDarkButton = Swal.mixin({
    customClass: {
        confirmButton: 'btn btn-danger w-100 rounded btn-outline-dark'
    },
    imageUrl: '/img/oops.png',
    timer: 3000,
    buttonsStyling: false
});