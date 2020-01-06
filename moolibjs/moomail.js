const nodemailer = require('nodemailer');

// create reusable transporter object using the default SMTP transport
let transporter = nodemailer.createTransport({
	host: 'in-v3.mailjet.com',
	port: 587,
	secure: false, // true for 465, false for other ports
	auth: {
		user: "ed2be92f75a79418e7e7e9e8617ce968", 
		pass: "33d150a5d4dffc8109201c0e486ace94"
	}
});

// send mail with defined transport object
const sendMail = function(mailOptions) {	
	transporter.sendMail(mailOptions, (error, info) => {
		if (error) {
			return console.log(error);
		}
		console.log('Message sent: %s', info.messageId);
	});
};

module.exports = {
	sendMail,
};