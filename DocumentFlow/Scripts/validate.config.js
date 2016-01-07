$(document).ready(function() {
    $("#login-form").validate({
        rules: {
            Email: {
                required: true,
                email: true
            },
            Password: {
                required: true,
                minlength: 6
            }
        },
        messages: {
            Password: {
                required: "Пожалуйста введите пароль.",
                minlength: "Ваш пароль должен содержать минимум 6 символов."
            },
            Email: {
                required: "Пожалуйста введите ваш электронный адрес.",
                email: "Пожалуйста введите правильны адрес электронной почты."
            }

        },

        submitHandler: function(form) {
            form.submit();
        }
    });
    $("#reg-form").validate({
        rules: {
            FirstName: "required",
            LastName: "required",
            Patronymic: "required",
            Login: {
                required: true,
                minlength: 3
            },
            Password: {
                required: true,
                minlength: 6,
                maxlength: 16
            },
            PasswordConfirm: {
                required: true,
                minlength: 6,
                maxlength: 16,
                equalTo: "#Password"
            },
            Email: {
                required: true,
                email: true
            }
        },
        messages: {
            Password: {
                required: "Пожалуйста введите пароль.",
                minlength: "Ваш пароль должен содержать минимум 6 символов.",
                maxlength: "Ваш пароль не может содержать более 16 символов."

            },
            FirstName: {
                required: "Пожалуйста введите ваше имя."

            },
            LastName: {
                required: "Пожалуйста введите вашу фамилию."

            },
            Patronymic: {
                required: "Пожалуйста введите ваше отчество."

            },
            Email: {
                required: "Пожалуйста введите ваш электронный адрес.",
                email: "Пожалуйста введите правильны адресс электронной почты."
            },
            PasswordConfirm: {
                required: "Пожалуйста введите подтверждение пароля.",
                equalTo: "Пароли не совпадают.",
                minlength: "Ваш пароль должен содержать минимум 6 символов.",
                maxlength: "Ваш пароль не может содержать более 16 символов."
            }

        },

        submitHandler: function(form) {
            form.submit();
        }
    });

    $("#edit-role").validate({
        rules: {
            Name: {
                required: true,
                minlength: 5
            }
        },
        messages: {
            Name: {
                required: "Пожалуйста введите название роли.",
                minlength: "Название должно быть минимум 5 символов."
            }

        },

        submitHandler: function(form) {
            form.submit();
        }
    });

    $("#edit-position").validate({
        rules: {
            Name: {
                required: true,
                minlength: 5
            }
        },
        messages: {
            Name: {
                required: "Пожалуйста введите название должности.",
                minlength: "Название должно быть минимум 5 символов."
            }

        },

        submitHandler: function(form) {
            form.submit();
        }
    });

    $("#edit-document-type").validate({
        rules: {
            Name: {
                required: true,
                minlength: 5
            }
        },
        messages: {
            Name: {
                required: "Пожалуйста введите название типа.",
                minlength: "Название должно быть минимум 5 символов."
            }

        },

        submitHandler: function(form) {
            form.submit();
        }
    });

    $.validator.setDefaults({
        ignore: []
    });

    $("#edit-document-template").validate({
        rules: {
            Name: {
                required: true,
                minlength: 5
            },
            PositionsPath: {
                required: true
            },
            Text: {
                required: true
            }
        },
        messages: {
            Name: {
                required: "Пожалуйста введите название шаблона.",
                minlength: "Название должно быть минимум 5 символов."
            },
            PositionsPath: {
                required: "Добавьте хотя бы одного получателя."
            },
            Text: {
                required: "Шаблон не может быть пустым."
            }

        },
        errorPlacement: function(error, element) {
            if (element.attr("name") === "PositionsPath")
                error.insertAfter(".selection");
            else if (element.attr("name") === "Name")
                error.insertAfter("#Name");
            else if (element.attr("name") === "Text")
                error.insertAfter(".fr-box");
        },

        submitHandler: function(form) {
            form.submit();
        }
    });

    $("#fill-form").validate();

    $("#fill-form").find("input.required, textarea.required, select.required").each(function() {

        $(this).rules("add", {
            required: true,
            messages: {
                required: "Поле не должно быть пустым"
            }
        });
    });

    $("#pass-change").validate({
        rules: {
            OldPassword: {
                required: true
            },
            NewPassword: {
                required: true,
                minlength: 6,
                maxlength: 16
            },
            PasswordConfirm: {
                required: true,
                minlength: 6,
                maxlength: 16,
                equalTo: "#NewPassword"
            }
        },
        messages: {
            OldPassword: {
                required: "Пожалуйста введите старый пароль."
            },
            NewPassword: {
                required: "Введите новый пароль.",
                minlength: "Ваш пароль должен содержать минимум 6 символов.",
                maxlength: "Ваш пароль не может содержать более 16 символов."
            },
            PasswordConfirm: {
                required: "Подтвердите пароль.",
                minlength: "Ваш пароль должен содержать минимум 6 символов.",
                maxlength: "Ваш пароль не может содержать более 16 символов."
            }

        }
    });

});